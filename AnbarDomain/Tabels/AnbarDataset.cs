using System;
using System.Data;
using System.Linq;

namespace AnbarDomain.Tabels
{


    partial class AnbarDataSet
    {
        partial class ProductsDataTable
        {
            public override void EndInit()
            {
                base.EndInit();

                TableNewRow += ProductsDataTable_TableNewRow;
            }

            //Important 
            private void ProductsDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                if (e.Row is ProductsRow row)
                {
                    if (row.IsCreatedAtNull())
                        row.CreatedAt = DateTime.Now;
                }



            }
        }



    }
}