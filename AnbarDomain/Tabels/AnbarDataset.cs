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
        partial class view_WarehouseReceiptsDataTable : IEnhancedDataTableMetadata
        {
            public string tableName => "WarehouseReceipts";
            public string viewName => "view_WarehouseReceipts";

            public override void EndInit()
            {
                base.EndInit();

                try
                {
                    if (this.Columns.Contains(PartyNameColumn.ColumnName))
                    {
                        this.PartyNameColumn.ExtendedProperties["IsViewColumn"] = true;
                    }

                    if (this.Columns.Contains(PartyTypeColumn.ColumnName))
                    {
                        this.PartyTypeColumn.ExtendedProperties["IsViewColumn"] = true;
                    }


                    if (this.Columns.Contains(WarehouseNameColumn.ColumnName))
                    {
                        this.WarehouseNameColumn.ExtendedProperties["IsViewColumn"] = true;
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"EndInit failed: {ex.Message}");
                    throw;
                }
            }

        }

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

                    if (this.Columns.Contains(TotalAmountColumn.ColumnName))
                    {
                        this.TotalAmountColumn.ExtendedProperties["IsViewColumn"] = true;
                    }
                    if (this.Columns.Contains(TotalAmountCalculatedColumn.ColumnName))
                    {
                        this.TotalAmountCalculatedColumn.ExtendedProperties["IsViewColumn"] = true;
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

    }
}
namespace AnbarDomain.Tabels.AnbarDataSetTableAdapters
{
    partial class view_WarehouseReceiptsTableAdapter
    {
        public DbDataAdapter GetAdapter() => this.Adapter;

    }

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
