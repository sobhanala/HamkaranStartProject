//using System;
//using System.Data;
//using Domain.SharedSevices;

//namespace Domain.Attribute
//{
//    public class AuditableDataRow : DataRow, IAuditable
//    {
//        private ISessionService _sessionService;

//        protected internal AuditableDataRow(DataRowBuilder builder) : base(builder)
//        {
//        }

//        public void InitializeAudit(ISessionService sessionService)
//        {
//            _sessionService = sessionService;
//        }

//        public void SetAuditFields(ISessionService sessionService)
//        {
//            if (_sessionService == null)
//            {
//                _sessionService = sessionService;
//            }

//            if (RowState == DataRowState.Added)
//            {
//                if (Table.Columns.Contains("CreatedAt") && this["CreatedAt"] == DBNull.Value)
//                    this["CreatedAt"] = DateTime.Now;

//                if (Table.Columns.Contains("CreatedBy") && this["CreatedBy"] == DBNull.Value && _sessionService?.CurrentUser != null)
//                    this["CreatedBy"] = _sessionService.CurrentUser.Id;
//            }
//            else if (RowState == DataRowState.Modified)
//            {
//                if (Table.Columns.Contains("UpdatedAt"))
//                    this["UpdatedAt"] = DateTime.Now;

//                if (Table.Columns.Contains("UpdatedBy") && _sessionService?.CurrentUser != null)
//                    this["UpdatedBy"] = _sessionService.CurrentUser.Id;
//            }
//        }
//    }
//}