using System;
using System.Data;
using System.Linq;
using Domain.Attribute;

namespace AnbarDomain.Tabels
{


    partial class AnbarDataSet
    {
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
        partial class WarehouseReceiptsDataTable:IAuditable
        {
            
        }
        

    }
}