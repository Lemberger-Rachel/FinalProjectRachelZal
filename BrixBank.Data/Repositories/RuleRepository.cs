using BrixBank.Data.Entities;
using BrixBank.Services.Interfaces;
using BrixBank.Services.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrixBank.Data.Repositories
{
    public class RuleRepository : IRuleRepository
    {
        private readonly BrixBankContext _context;
        public RuleRepository(BrixBankContext context)
        {
            _context = context;
        }
        public void ReadExcelFile()
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                string custId = "";
                var listRule = new List<string>();
                //open the existing excel file and read through its content . Open the excel using openxml sdk
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(@"C:\Users\RLemberger\Documents\Brix\FinalProject\BrixBank\BrixBank.Data\bin\Debug\netcoreapp3.1\book1.xlsx", false))
                {
                    //create the object for workbook part  
                    WorkbookPart workbookPart = doc.WorkbookPart;
                    Sheets thesheetcollection = workbookPart.Workbook.GetFirstChild<Sheets>();
                    StringBuilder excelResult = new StringBuilder();

                    //using for each loop to get the sheet from the sheetcollection  
                    foreach (Sheet thesheet in thesheetcollection)
                    {
                        excelResult.AppendLine("Excel Sheet Name : " + thesheet.Name);
                        //statement to get the worksheet object by using the sheet id  
                        Worksheet theWorksheet = ((WorksheetPart)workbookPart.GetPartById(thesheet.Id)).Worksheet;
                        SheetData thesheetdata = (SheetData)theWorksheet.GetFirstChild<SheetData>();
                        foreach (Row thecurrentrow in thesheetdata)
                        {
                            foreach (Cell thecurrentcell in thecurrentrow)
                            {
                                //statement to take the integer value  
                                string currentcellvalue = string.Empty;
                                if (thecurrentcell.DataType != null)
                                {
                                    if (thecurrentcell.DataType == CellValues.SharedString)
                                    {
                                        int id;
                                        if (Int32.TryParse(thecurrentcell.InnerText, out id))
                                        {
                                            SharedStringItem item = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
                                            listRule.Add(item.InnerText.ToString());
                                            custId = thesheet.Name;
                                            if (item.Text != null)
                                            {
                                                //code to take the string value  
                                                excelResult.Append(item.Text.Text + " ");
                                            }
                                            else if (item.InnerText != null)
                                            {
                                                currentcellvalue = item.InnerText;
                                            }
                                            else if (item.InnerXml != null)
                                            {
                                                currentcellvalue = item.InnerXml;
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    excelResult.Append(Convert.ToInt16(thecurrentcell.InnerText) + " ");
                                }
                            }
                            excelResult.AppendLine();
                        }
                        excelResult.Append("");
                    }
                }
                InsertToDB(custId, listRule);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void InsertToDB(string custId, List<string> listRule)
        {
            try
            {
                Customer customer = _context.Customers.FirstOrDefault(c => c.Name == custId);
                if (customer == null)
                {
                    customer = new Customer();
                    customer.Name = custId;
                    _context.Customers.Add(customer);
                    _context.SaveChanges();
                }
                foreach (var item in listRule)
                {
                    Rules rule = new Rules();
                    string itemParse = ParseExp(item);
                    string[] collection = itemParse.Split(';');
                    rule.Kind = collection[0];
                    rule.Operator = collection[1];
                    if (Int32.TryParse(collection[2], out int j) == true)
                    {
                        rule.Output = Int32.Parse(collection[2]);
                    }
                    else
                    {
                        string itemParseManager = collection[2].Replace("=>", ";" + "=>" + ";");
                        string[] collection2 = itemParseManager.Split(';');
                        rule.Output = Int32.Parse(collection2[0]);
                        rule.IsManager = collection2[2];
                        rule.Law = collection[0] + collection[1] + collection2[0];
                        rule.CustomerId2 = customer;
                        var IsExists2 = _context.Rules.FirstOrDefault(r => r.Kind == rule.Kind && r.CustomerId2.CustomerId == rule.CustomerId2.CustomerId && r.IsManager != null);
                        if (IsExists2 != null)
                        {
                            _context.Rules.Remove(IsExists2);
                        }
                        _context.Rules.Add(rule);
                        _context.SaveChanges();
                        continue;
                    }
                    rule.Law = item;
                    rule.CustomerId2 = customer;
                    var IsExists = _context.Rules.FirstOrDefault(r => r.Kind == rule.Kind && r.CustomerId2.CustomerId == rule.CustomerId2.CustomerId);
                    if (IsExists != null)
                    {
                        _context.Rules.Remove(IsExists);
                    }
                    _context.Rules.Add(rule);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static string ParseExp(string expr)
        {
            string[] ops = { ">=", "<=", "==", "<", ">" };
            string passExpr = expr;
            foreach (var op in ops)
            {
                passExpr = expr.Replace(op, ";" + op + ";");
                if (!passExpr.Equals(expr))
                {
                    break;
                }
            }
            return passExpr;
        }
        public bool ToCheck(LoanRequestModel loanRequestModel)
        {
            Customer customer = _context.Customers.FirstOrDefault(c => c.CustomerId == loanRequestModel.LoanSupplied);
            var listRule = _context.Rules.Where(item => item.CustomerId2.CustomerId == loanRequestModel.LoanSupplied && item.IsManager == null).ToList();
            RuleNode treeRule = BuidlTree(listRule);
            return Eval(loanRequestModel.DictionaryData, treeRule);
        }
        public RuleNode BuidlTree(List<Rules> rules)
        {
            if (rules.Count == 1)
            {
                var ruleNode2 = new RuleNode();
                ruleNode2.rule = rules[0];
                return ruleNode2;
            }
            var ruleNode = new RuleNode();
            ruleNode.rule = new BaseRule();
            ruleNode.rule.Operator = "AND";
            ruleNode.Left = new RuleNode();
            ruleNode.Left.rule = rules[0];
            rules.RemoveAt(0);
            ruleNode.Right = BuidlTree(rules);
            return ruleNode;
        }
        public bool EvalRule(int dataValue, Rules rule)
        {
            switch (rule.Operator)
            {
                case ">=":
                    return dataValue >= rule.Output;
                case "<=":
                    return dataValue <= rule.Output;
                case "<":
                    return dataValue < rule.Output;
                case ">":
                    return dataValue > rule.Output;
                case "==":
                    return dataValue == rule.Output;
                default:
                    return false;
            }
        }
        public bool Eval(Dictionary<string, int> data, RuleNode root)
        {
            var rule = root.rule as Rules;
            if (rule != null)
            {
                return EvalRule(data[rule.Kind], rule);
            }
            else
            {
                return Eval(data, root.Left) && Eval(data, root.Right);
            }
        }
        public bool Manager(LoanRequestModel loanRequestModel)
        {
            Customer customer = _context.Customers.FirstOrDefault(c => c.CustomerId == loanRequestModel.LoanSupplied);
            var itemRule = _context.Rules.Where(item => item.CustomerId2.CustomerId == loanRequestModel.LoanSupplied).ToList();
            var listRuleManager = itemRule.Where(item => item.IsManager != null).ToList();
            foreach (var itemManager in listRuleManager)
            {
                itemRule.RemoveAll(item =>item.Kind ==itemManager.Kind);
            }
            itemRule.AddRange(listRuleManager);
            RuleNode treeRule = BuidlTree(itemRule);
            return Eval(loanRequestModel.DictionaryData, treeRule);
        }
    }
}
