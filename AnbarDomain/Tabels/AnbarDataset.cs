using Domain.Attribute;
using Domain.Repositorys;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace AnbarDomain.Tabels
{


    partial class AnbarDataSet
    {
        partial class InventoryDataTable : IEnhancedDataTableMetadata
        {
            public string tableName => "Inventory";
            public string viewName => "Inventory";
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
                    }
                    if (this.Columns.Contains(ReceiptIdColumn.ColumnName))
                    {
                        this.TotalAmountColumn.ExtendedProperties["IsViewColumn"] = true;
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


        partial class WarehouseReceiptItemsDataTable
        {
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

            }
        }
        partial class WarehouseReceiptsDataTable : IAuditable
        {
            private void changetheRow()
            {



            }
        }


    }
}
namespace AnbarDomain.Tabels.AnbarDataSetTableAdapters
{
    partial class InventoryTableAdapter
    {
        public DbDataAdapter GetAdapter() => this.Adapter;

    }

    partial class WarehousesTableAdapter
    {
    }

    partial class WarehouseReceiptItemsWithProductViewTableAdapter
    {
        public DbDataAdapter GetAdapter() => this.Adapter;


    }

    partial class ProductsTableAdapter
    {
    }

    public partial class WarehouseReceiptsTableAdapter
    {

        public DbDataAdapter GetAdapter() => this.Adapter;
    }
}

namespace AnbarDomain.Tabels.AnbarDataSetTableAdapters
{


    public partial class WarehousesTableAdapter
    {
    }
}
