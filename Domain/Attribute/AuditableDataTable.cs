//using System;
//using System.Data;
//using Domain.SharedSevices;

//namespace Domain.Attribute
//{
//    public class AuditableDataTable : DataTable
//    {
//        private ISessionService _sessionService;

//        public AuditableDataTable()
//        {
//            TableNewRow += AuditableDataTable_TableNewRow;
//            RowChanged += AuditableDataTable_RowChanged;
//        }

//        public void SetSessionService(ISessionService sessionService)
//        {
//            _sessionService = sessionService;
//        }

//        protected override Type GetRowType()
//        {
//            return typeof(AuditableDataRow);
//        }

//        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
//        {
//            AuditableDataRow row = (AuditableDataRow)base.NewRowFromBuilder(builder);
//            row.InitializeAudit(_sessionService);
//            return row;
//        }

//        private void AuditableDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
//        {
//            if (e.Row is IAuditable auditableRow)
//            {
//                auditableRow.SetAuditFields(_sessionService);
//            }
//        }

//        private void AuditableDataTable_RowChanged(object sender, DataRowChangeEventArgs e)
//        {
//            if ((e.Action == DataRowAction.Add || e.Action == DataRowAction.Change) &&
//                e.Row is IAuditable auditableRow)
//            {
//                auditableRow.SetAuditFields(_sessionService);
//            }
//        }
//    }
//}
