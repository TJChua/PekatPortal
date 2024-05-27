using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using DevExpress.ExpressApp.Web.Utils;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PekatPortal.Module.Web.Controllers
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ListViewSize : ViewController<ListView>
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      //private string _PersistentProperty;
      //[XafDisplayName("My display name"), ToolTip("My hint message")]
      //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
      //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
      //public string PersistentProperty {
      //    get { return _PersistentProperty; }
      //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
      //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            if (View.Id == "vw_CustomerList_LookupListView" || View.Id == "vw_ItemList_LookupListView" || View.Id == "vw_SalesPerson_LookupListView")
            {
                ASPxGridListEditor editor = View.Editor as ASPxGridListEditor;
                if (editor == null) return;
                ASPxGridView grid = editor.Grid;
                //int gridSize = 0;
                grid.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.EndlessPaging;
                grid.SettingsPager.PageSize = 50;
                grid.Settings.VerticalScrollBarMode = ScrollBarMode.Auto;
                grid.Settings.VerticalScrollableHeight = 500;
                //grid.Settings.UseFixedTableLayout = true;
                //grid.Settings.HorizontalScrollBarMode = ScrollBarMode.Visible;

                //grid.SettingsResizing.ColumnResizeMode = ColumnResizeMode.Disabled;

                //foreach (WebColumnBase column in grid.Columns)
                //{
                //    IColumnInfo columnInfo = ((IDataItemTemplateInfoProvider)editor).GetColumnInfo(column);
                //    if (columnInfo != null)
                //    {
                //        IModelColumn modelColumn = (IModelColumn)columnInfo.Model;

                //        if (column.Caption.Contains("NAME"))
                //        {
                //            column.Width = Unit.Pixel(500);
                //            gridSize += 500;
                //        }
                //        else
                //        {
                //            column.Width = Unit.Pixel(200);
                //            gridSize += 200;
                //        }
                //    }
                //}
                //grid.Width = Unit.Pixel(gridSize);
                //grid.SettingsPopup.HeaderFilter.Width = Unit.Pixel(gridSize);
            }
            //    grid.ClientInstanceName = "grid_" + View.Id;
            //    string AdjustSize = String.Format("var height = Math.max(0, document.documentElement.clientHeight) - 270;{0}.SetHeight(height);", grid.ClientInstanceName);
            //    ASPxGlobalEvents globalEvents = new ASPxGlobalEvents();
            //    globalEvents.ID = "GE1";
            //    globalEvents.ClientSideEvents.ControlsInitialized = string.Format("function(s,e){{ ASPxClientUtils.AttachEventToElement(window, 'resize', function(evt) {{{0}}}); }}", AdjustSize);
            //    ClientSideEventsHelper.AssignClientHandlerSafe(grid, "Init", string.Format("function(s,e){{{0}}}", AdjustSize), "T473350");
            //    ClientSideEventsHelper.AssignClientHandlerSafe(grid, "EndCallback", string.Format("function(s,e){{{0}}}", AdjustSize), "T473350");
            //    grid.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.EndlessPaging;
            //    //grid.SettingsPager.PageSize = 50;
            //    grid.Settings.VerticalScrollBarMode = ScrollBarMode.Auto;
            //    grid.Settings.VerticalScrollableHeight = 500;
            //    grid.Settings.HorizontalScrollBarMode = ScrollBarMode.Auto;
            //    grid.Width = Unit.Pixel(2000);
            //    //grid.
            //    for (int i = 0; i < grid.Columns.Count; i++)
            //    {
            //        var width = 0;
            //        if (grid.Columns[i].Caption.Contains("NAME")) width = 500;
            //        else width = 250;
            //        //calculate the required width  
            //        grid.Columns[i].MinWidth = width;
            //        grid.Columns[i].Width = Unit.Pixel(width);
            //    }
            //    //foreach (WebColumnBase column in grid.Columns)
            //    //{
            //    //    IColumnInfo columnInfo = ((IDataItemTemplateInfoProvider)editor).GetColumnInfo(column);
            //    //    if (columnInfo != null)
            //    //    {
            //    //        IModelColumn modelColumn = (IModelColumn)columnInfo.Model;
            //    //        if (column.Caption.Contains("NAME")) column.Width = Unit.Pixel(500);
            //    //        else column.Width = Unit.Pixel(350);
            //    //    }
            //    //}
            //    //grid.EnablePagingGestures = AutoBoolean.False;
            //    ((Control)View.Control).Controls.Add(globalEvents);
            //} 
        }
    }
}