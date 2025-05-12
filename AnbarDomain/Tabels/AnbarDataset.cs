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



                    if (row.IsProductCodeNull() || string.IsNullOrWhiteSpace(row.ProductCode))
                    {
                        int maxCode = this.AsEnumerable()
                            .Where(r => !r.IsProductCodeNull() && int.TryParse(r.ProductCode, out _))
                            .Select(r => int.Parse(r.ProductCode))
                            .DefaultIfEmpty(0)
                            .Max();

                        row.ProductCode = (maxCode + 1).ToString("D6");

                    }
                }



            }
        }



    }
}