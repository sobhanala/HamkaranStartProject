using Domain.Attribute;
using Domain.Repositorys;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace AnbarDomain.Tabels
{


    partial class AnbarDataSet : IDatasetMetaData<AnbarDataSet.view_WarehouseReceiptsDataTable, AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable>
    {
        public view_WarehouseReceiptsDataTable GetHeaderTable()
        {
            return this.view_WarehouseReceipts;
        }

        public WarehouseReceiptItemsWithProductViewDataTable GetDetailTable()
        {
            return this.WarehouseReceiptItemsWithProductView;
        }

        partial class view_WarehouseReceiptsDataTable : IEnhancedDataTableMetadata
        {
            public string tableName => "WarehouseReceipts";
            public string viewName => "view_WarehouseReceipts";
            public string fkMasterDetail => "";

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
            public string fkMasterDetail => "";

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



                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"EndInit failed: {ex.Message}");
                    throw;
                }
            }

            public string tableName => "WarehouseReceiptItems";
            public string viewName => "WarehouseReceiptItemsWithProductView";
            public string fkMasterDetail => "ReceiptId";
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



    partial class WarehouseReceiptItemsWithProductViewTableAdapter
    {
        public DbDataAdapter GetAdapter() => this.Adapter;


    }



}

