using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Domain.Attribute;

namespace AnbarDomain.Tabels
{


    partial class AnbarDataSet
    {


        partial class WarehouseReceiptItemsDataTable
        {
        }

        partial class WarehouseReceiptItemsWithProductViewDataTable
        {
            public override void EndInit()
            {
                base.EndInit();

                try
                {
                    if (this.Columns.Contains("ProductCode"))
                    {
                        this.ProductCodeColumn.ReadOnly = true;
                        this.ProductCodeColumn.ExtendedProperties["IsViewColumn"] = true;
                    }

                    if (this.Columns.Contains("ProductName"))
                    {
                        this.ProductNameColumn.ReadOnly = true;
                        this.ProductNameColumn.ExtendedProperties["IsViewColumn"] = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"EndInit failed: {ex.Message}");
                    throw;
                }
            }
        }

        partial class WarehouseReceiptsDataTable
        {
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
namespace AnbarDomain.AnbarDataSetTableAdapters {
    
    
    public partial class WarehouseReceiptsTableAdapter {
    }
}
