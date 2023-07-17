using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using DevExpress.Persistent.Base;

namespace XAFExample.Module.BusinessObjects.nwind
{
    [DefaultClassOptions, ImageName("BO_Contact")]
    public partial class Customers
    {
        public Customers(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
