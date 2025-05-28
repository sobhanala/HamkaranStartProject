using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Domain.Attribute;
using Domain.Repositorys;

namespace AnbarDomain.Tabels
{


    partial class AnbarDataSet
    {


        partial class WarehouseReceiptItemsDataTable
        {
        }

        partial class WarehouseReceiptItemsWithProductViewDataTable : IEnhancedDataTableMetadata
        {
            public override void EndInit()
            {
                base.EndInit();

                try
                {
                    if (this.Columns.Contains(ProductCodeColumn.ColumnName))
                    {
                        this.ProductCodeColumn.ExtendedProperties["IsViewColumn"] = true;
                    }

                    if (this.Columns.Contains(ProductNameColumn.ColumnName))
                    {
                        this.ProductNameColumn.ExtendedProperties["IsViewColumn"] = true;
                    }

                    if (this.Columns.Contains(ReceiptIdColumn.ColumnName))
                    {
                        ReceiptIdColumn.ReadOnly = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"EndInit failed: {ex.Message}");
                    throw;
                }
            }

            public string tableName => "WarehouseReceiptItems";
            public string viewName => "WarehouseReceiptItemsWithProductView";
        }

        partial class WarehouseReceiptsDataTable : IEnhancedDataTableMetadata
        {
            public string tableName => this.TableName;
            public string viewName => this.TableName;
        }

        partial class WarehousesDataTable
        {
        }

        partial class ProductsDataTable : IAuditable
        {
            public override void EndInit()
            {
                base.EndInit();

                TableNewRow += ProductsDataTable_TableNewRow;
            }

            //Important 
            private void ProductsDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                //if (e.Row is ProductsRow row)
                //{

                //    if (row.IsCreatedAtNull())
                //        row.CreatedAt = DateTime.Now;
                //}



            }
        }
        partial class WarehouseReceiptsDataTable : IAuditable
        {

        }


    }
}
namespace AnbarDomain.Tabels.AnbarDataSetTableAdapters
{
    partial class WarehousesTableAdapter
    {
    }

    partial class WarehouseReceiptItemsWithProductViewTableAdapter
    {
        public SqlConnection GetConnection() => this.Connection;
        public DbDataAdapter GetAdapter() => this.Adapter;


    }

    partial class ProductsTableAdapter
    {
    }

    public partial class WarehouseReceiptsTableAdapter
    {

            public SqlConnection GetConnection() => this.Connection;
            public DbDataAdapter GetAdapter() => this.Adapter;
    }
}

namespace AnbarDomain.Tabels.AnbarDataSetTableAdapters
{


    public partial class WarehousesTableAdapter
    {
    }
}
