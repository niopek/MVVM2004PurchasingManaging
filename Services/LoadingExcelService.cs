using MVVM2004PurchasingManaging.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM2004PurchasingManaging.Services;

public static class LoadingExcelService
{
    public static async Task<DataTable> GetDataTableFromExcel(string filePath)
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
        DataTable dataTable = new();
        await Task.Run(() =>
        {
            try
            {
                using OleDbConnection oleDbConnection = new(connectionString);
                try
                {
                    oleDbConnection.Open();

                    DataTable? dtSchema = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    string? Sheet1 = dtSchema.Rows[0].Field<string>("TABLE_NAME");

                    using OleDbDataAdapter objDA = new($"select * from [{Sheet1}]", connectionString);
                    try
                    {
                        objDA.Fill(dataTable);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        });
        return dataTable;

    }

    public static async Task<List<Indeks>> DataTableWithIndeksesToList(DataTable dataTable)
    {
        List<Indeks> listOfIndeks = new();
        if (dataTable.Columns.Count > 3)
        {
            await Task.Run(() =>
            {
                foreach (DataRow dataRow in dataTable.AsEnumerable())
                {
                    if (dataRow[0].ToString() != "" && dataRow[0].ToString() != null &&                        //        
                    dataRow[1].ToString() != "" && dataRow[1].ToString() != null &&                            //        
                                                   dataRow[2].ToString() != null &&                            //        
                    dataRow[3].ToString() != "" && dataRow[3].ToString() != null &&                            //        
                    dataRow[4].ToString() != "" && dataRow[4].ToString() != null)
                    {

                        int IndeksId = int.Parse(dataRow[0].ToString()!);
                        string IndeksName = dataRow[1].ToString()!;
                        string IndeksDescription = dataRow[2].ToString();
                        string IndeksUnitOfMeasure = dataRow[3].ToString()!;
                        string IndeksTc = dataRow[4].ToString()!;


                        Indeks indeksToAdd = new() { Id = IndeksId, Name = IndeksName, Description = IndeksDescription, UnitOfMeasure = IndeksUnitOfMeasure, Tc = IndeksTc };
                        try
                        {
                            listOfIndeks.Add(indeksToAdd);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Błąd przy dodawaniu indeksu {indeksToAdd.Id}");
                        }



                    }
                }
            });

        }
        else
        {
            MessageBox.Show("Błedny format raportu");
        }
        return listOfIndeks;
    }

    public static async Task<List<Supplier>> DataTableWithSuppliersToList(DataTable dataTable)
    {
        List<Supplier> listOfSuppliers = new();
        if (dataTable.Columns.Count > 1)
        {
            await Task.Run(() =>
            {
                foreach (DataRow dataRow in dataTable.AsEnumerable())
                {
                    if (dataRow[0].ToString() != "" && dataRow[0].ToString() != null &&                        //        
                    dataRow[1].ToString() != "" && dataRow[1].ToString() != null)
                    {

                        int supplierId = int.Parse(dataRow[0].ToString()!);
                        string supplierName = dataRow[1].ToString()!;

                        Supplier supplierToAdd = new() { SupplierId = supplierId, SupplierName = supplierName };
                        try
                        {
                            listOfSuppliers.Add(supplierToAdd);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Błąd przy dodawaniu indeksu {supplierToAdd.SupplierId}");
                        }



                    }
                }
            });

        }
        else
        {
            MessageBox.Show("Błedny format raportu");
        }
        return listOfSuppliers;
    }
}
