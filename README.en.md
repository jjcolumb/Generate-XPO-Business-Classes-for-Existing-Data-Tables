
[![es](https://img.shields.io/badge/lang-es-yellow.svg)](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/blob/master/README.md)

# XAF-XPO-DataModel


# Generate XPO Business Classes for Existing Data Tables


From other documentation sources, you learned how to create business classes for your XAF applications. If you have business classes in your application, you have database tables in the application’s database. However, the reality is that most programmers are not building new applications from scratch, but maintaining existing databases. In this instance, they can use the  [XPO Data Model Wizard](https://docs.devexpress.com/XPO/14810/design-time-features/data-model-wizard)  that generates a business model declaration for the specified legacy database. Follow the steps below to generate business classes for your existing database that you are going to use in your XAF application.

If you prefer to watch a video rather than walk through these step-by-step instructions, you can access a corresponding tutorial in our YouTube Channel:  [XAF: Create an Application Based on the Existing Database](https://www.youtube.com/watch?v=vw5ZnJ-9Iyw).

## Generate an XPO Data Model

-   Create a new XAF solution using the  **DevExpress v23.1  XAF Template Gallery**  template.
-   Right-click the  _BusinessObjects_  folder located in the  [module project](https://docs.devexpress.com/eXpressAppFramework/118045/application-shell-and-base-infrastructure/application-solution-components/application-solution-structure). Choose  **Add**  |  **New Item**. In the invoked  **Add New Item**  dialog, choose the  **DevExpress ORM Data Model Wizard**  template located in the  **DevExpress**  category. Set the new item’s name to  **MySolutionDataModel.xpo**  and click  **Add**. You will see that the  _MySolutionDataModel.xpo_  item is added and the wizard dialog is invoked.
-   In the invoked wizard dialog, choose  **Map to an existing database**  and click  **Next**.
    
    ![image](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/assets/126447472/2ff59958-9775-46c7-a1ee-7f9f27be5902)

    
-   Specify connection settings to the database containing the target data. Multiple database systems (Microsoft SQL Server, DB2, MySql, Firebird, etc) are supported by the wizard. Use the  **Provider**  combo box to select the required database type. Note that the corresponding database provider assembly must be registered in the Global Assembly Cache (GAC) on your machine or the wizard will fail. In this example, we will use the “Northwind Traders” demo database. This database is shipped with DXperience and installed in  _%PUBLIC%\Documents\DevExpress Demos  23.1  \Components\Data\nwind.mdb_, by default.
    
    ![image](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/assets/126447472/441bcb7a-1ceb-4e31-8a6b-e3c734d48c78)

    
    Click  **Next**  after connection settings have been specified.
    
-   In the next step, the wizard displays a list of tables that can be mapped to persistent classes. Select a table(s) to be mapped to a persistent object(s) and for each table select columns that will be mapped to the persistent object’s properties. For unchecked columns, persistent properties will not be generated. For instance, select the  **Customers**  and  **Orders**  tables.
    
    ![image](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/assets/126447472/8b2266ac-da6d-4a0f-b941-7adb4dba251e)

    
-   Click  **Next**  to close the wizard. The generated data model will be shown in the  **XPO Data Model**  designer.
    
    ![image](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/assets/126447472/72d3c6fb-5a53-421b-ab9c-2249d707dcd5)

    
    If you want to customize the generated data model, refer to the  [How to: Create a Business Model in the XPO Data Model Designer](https://docs.devexpress.com/eXpressAppFramework/113450/business-model-design-orm/business-model-design-with-xpo/create-a-business-model-in-the-xpo-data-model-designer)  topic. The names of persistent classes and their properties match the names of selected tables and their columns. In the current sample, the table names are in plural form. So, you may want to change class names (**Customers**  to  **Customer**  and  **Orders**  to  **Order**). To rename a class or its property, select it in the designer and change the  **Name**  in the  **Properties**  window. The classes and properties with modified names will still be mapped to corresponding tables and columns, as the  [PersistentAttribute](https://docs.devexpress.com/XPO/DevExpress.Xpo.PersistentAttribute)  is automatically added to the designer-generated code.
    
-   In the  **Visual Studio**  toolbar, click save. The generated code files will appear in the  **Solution Explorer**, in the  _BusinessObjects\MySolutionDataModelCode_  folder.
    
    ![image](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/assets/126447472/f8a64016-55fa-435f-828f-f2b83e647b2f)

    
    >NOTE
    If you do not like to deal with the designer and prefer to do everything in code, create a separate code file and copy the generated classes into it. Then, remove files that are added by the designer.
    

## Add XAF-Specific Attributes in Code

-   Open the  _Customer.cs_  (_Customer.vb_) file. Decorate the  **Customer**  class with the  [DefaultClassOptionsAttribute](https://docs.devexpress.com/eXpressAppFramework/DevExpress.Persistent.Base.DefaultClassOptionsAttribute)  and  [ImageNameAttribute](https://docs.devexpress.com/eXpressAppFramework/DevExpress.Persistent.Base.ImageNameAttribute)  attributes. As the result, the  **Customer**  object will be added to the  [Navigation System](https://docs.devexpress.com/eXpressAppFramework/113198/application-shell-and-base-infrastructure/navigation-system)  and an icon from the built-in library will be associated with this object.

    
    ```csharp
    using DevExpress.Persistent.Base;
    // ...
    [DefaultClassOptions, ImageName("BO_Contact")]
    public partial class Customer{
        public Customer(Session session) : base(session) { }
        public Customer() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
    
    ```
    
-   Open the  _Order.cs_  (_Order.vb_) file. Decorate the  **Order**  class with the  **DefaultClassOptions**  and  **ImageName**  attributes.
    

    
    ```csharp
    using DevExpress.Persistent.Base;
    // ...
    [DefaultClassOptions, ImageName("BO_Order")]
    public partial class Order {
        public Order(Session session) : base(session) { }
        public Order() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
    
    ```
    

You can add more custom code to the auto-generated classes (e.g., override base class methods). Do not change code located in files with the  _designer_  suffix - they contain designer-generated code and should not be modified manually. The generated classes are declared as  _partial_. Designed and custom class parts are located in different files.

>NOTE
It is recommended that you specify the [default property](https://docs.devexpress.com/eXpressAppFramework/113525/business-model-design-orm/how-to-specify-a-display-member-for-a-lookup-editor-detail-form-caption) for each generated class via the **System.ComponentModel.DefaultProperty** attribute. Default property is treated as a human-readable identifier in an XAF application UI.

>IMPORTANT
You cannot apply attributes to properties in the partial class’ code. Instead, use the designer (see the next section).

## Add XAF-Specific Attributes in the Designer

Alternatively, you can use the designer to apply attributes. Focus the requires class or field and specify the  **Custom Attributes**  setting in the  **Properties**  window.

![image](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/assets/126447472/18d1c572-3f81-43ae-ac2e-66fd27195e4e)


## Specify the Connection String

An empty XAF application is generated with a default connection string:  . You should change it, so that the application uses the required database. Change the connection string in the application configuration file. You can copy the valid connection string from the auto-created  _MySolution.Module\app.config_  file.`Data Source=(localdb)\mssqllocaldb;Initial Catalog=MyApplication;Integrated Security=SSPI;Pooling=false`

## Run the Application

Now you can run the WinForms and ASP.NET Web Forms applications to see the result. These applications are completely based on the business model generated for the legacy database.

-   [WinForms](https://docs.devexpress.com/eXpressAppFramework/113451/business-model-design-orm/business-model-design-with-xpo/generate-xpo-business-classes-for-existing-data-tables#tabpanel_+wrfC4bG6p-2_tabid-01)
-   [ASP.NET Web Forms](https://docs.devexpress.com/eXpressAppFramework/113451/business-model-design-orm/business-model-design-with-xpo/generate-xpo-business-classes-for-existing-data-tables#tabpanel_+wrfC4bG6p-2_tabid-02)

![image](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/assets/126447472/3f4348f8-cf90-45da-8b40-2f29884acb33)

