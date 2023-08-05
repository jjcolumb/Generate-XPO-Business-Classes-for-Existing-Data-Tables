[![en](https://img.shields.io/badge/lang-en-red.svg)](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/blob/master/README.en.md)

# Generar clases empresariales XPO para tablas de datos existentes


De otras fuentes de documentación, ha aprendido a crear clases de negocio para sus aplicaciones XAF. Si tiene clases de negocio en la aplicación, tiene tablas de base de datos en la base de datos de la aplicación. Sin embargo, la realidad es que la mayoría de los programadores no están construyendo nuevas aplicaciones desde cero, sino manteniendo las bases de datos existentes. En este caso, pueden utilizar el Asistente para modelos de datos  [XPO](https://docs.devexpress.com/XPO/14810/design-time-features/data-model-wizard)  que genera una declaración de modelo de negocio para la base de datos heredada especificada. Siga los pasos siguientes para generar clases de negocio para la base de datos existente que va a utilizar en la aplicación XAF.

Si prefiere ver un video en lugar de seguir estas instrucciones paso a paso, puede acceder a un tutorial correspondiente en nuestro canal de YouTube:  [XAF: Crear una aplicación basada en la base de datos  existente](https://www.youtube.com/watch?v=vw5ZnJ-9Iyw).

## Generar un modelo de datos XPO

-   Cree una nueva solución XAF con la plantilla  **DevExpress v23.1  XAF Template Gallery**.
-   Haga clic con el botón secundario en la carpeta  _BusinessObjects_  ubicada en el  [proyecto de módulo](https://docs.devexpress.com/eXpressAppFramework/118045/application-shell-and-base-infrastructure/application-solution-components/application-solution-structure). Elija  **Agregar**  |  **Nuevo artículo**. En el cuadro de diálogo invocado  **Agregar nuevo elemento**, elija la plantilla  **DevExpress ORM Data Model Wizard**  ubicada en la categoría  **DevExpress**. Establezca el nombre del nuevo elemento en  **MySolutionDataModel.xpo**  y haga clic en  **Agregar**. Verá que se agrega el elemento  _MySolutionDataModel.xpo_  y se invoca el cuadro de diálogo del asistente.
-   En el cuadro de diálogo del asistente invocado, elija  **Asignar a una base de datos existente**  y haga clic en  **Siguiente**.
    
    ![image](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/assets/126447472/5f5aa6f7-010e-4b31-acdd-1096ea1eb800)

-   Especifique la configuración de conexión a la base de datos que contiene los datos de destino. El asistente admite múltiples sistemas de bases de datos (Microsoft SQL Server, DB2, MySql, Firebird, etc.). Utilice el cuadro combinado  **Proveedor**  para seleccionar el tipo de base de datos necesario. Tenga en cuenta que el ensamblado del proveedor de base de datos correspondiente debe registrarse en la caché de ensamblados global (GAC) del equipo o se producirá un error en el asistente. En este ejemplo, usaremos la base de datos de demostración "Northwind Traders". Esta base de datos se incluye con DXperience y se instala en %_PUBLIC%\Documents\DevExpress Demos  23.1  \Components\Data\nwind.mdb_, de forma predeterminada.
    
    ![image](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/assets/126447472/e876db14-1f08-42b3-ad2c-7585b9319f3c)

    
    Haga clic en  **Siguiente**  después de especificar la configuración de conexión.
    
-   En el siguiente paso, el asistente muestra una lista de tablas que se pueden asignar a clases persistentes. Seleccione una tabla o tablas que se asignarán a un objeto persistente y, para cada tabla, seleccione las columnas que se asignarán a las propiedades del objeto persistente. Para las columnas sin marcar, no se generarán propiedades persistentes. Por ejemplo, seleccione las tablas  **Clientes**  y  **Pedidos**.
    
    ![image](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/assets/126447472/fd8173bb-6d78-4d44-b189-38c119c5566e)

    
-   Haga clic en  **Siguiente**  para cerrar el asistente. El modelo de datos generado se mostrará en el diseñador del  **modelo de datos XPO**.
    
    ![image](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/assets/126447472/9460b3ec-660f-49ce-80a9-7a48a6857188)

    
    Si desea personalizar el modelo de datos generado, consulte el tema  [Cómo: Crear un modelo de negocio en el Diseñador de modelos de datos XPO](https://docs.devexpress.com/eXpressAppFramework/113450/business-model-design-orm/business-model-design-with-xpo/create-a-business-model-in-the-xpo-data-model-designer). Los nombres de las clases persistentes y sus propiedades coinciden con los nombres de las tablas seleccionadas y sus columnas. En el ejemplo actual, los nombres de tabla están en plural. Por lo tanto, es posible que desee cambiar los nombres de clase (**Clientes**  a  **Cliente**  y  **Pedidos**  a  **pedido**). Para cambiar el nombre de una clase o su propiedad, selecciónela en el diseñador y cambie el  **Nombre**  en la ventana  **Propiedades**. Las clases y propiedades con nombres modificados se seguirán asignando a las tablas y columnas correspondientes, ya que  [PersistentAttribute](https://docs.devexpress.com/XPO/DevExpress.Xpo.PersistentAttribute)  se agrega automáticamente al código generado por el diseñador.
    
-   En la barra de herramientas de  **Visual Studio**, haga clic en guardar. Los archivos de código generados aparecerán en el  **Explorador de soluciones**, en la carpeta  _BusinessObjects\MySolutionDataModelCode_.
    
    ![image](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/assets/126447472/bc028b4d-9afc-420e-8009-1fffeafdce0d)

    
    >NOTA
    Si no le gusta tratar con el diseñador y prefiere hacer todo en código, cree un archivo de código separado y copie las clases generadas en él. A continuación, quite los archivos agregados por el diseñador.
    

## Agregar atributos específicos de XAF en el código

-   Abra el archivo Customer_.cs_  (_Customer.vb_). Decore la clase  **Customer**  con los atributos  [DefaultClassOptionsAttribute](https://docs.devexpress.com/eXpressAppFramework/DevExpress.Persistent.Base.DefaultClassOptionsAttribute)  e  [ImageNameAttribute](https://docs.devexpress.com/eXpressAppFramework/DevExpress.Persistent.Base.ImageNameAttribute). Como resultado, el objeto  **Customer**  se agregará al sistema de  [navegación](https://docs.devexpress.com/eXpressAppFramework/113198/application-shell-and-base-infrastructure/navigation-system)  y un icono de la biblioteca integrada se asociará a este objeto.
    

    
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
    
-   Abra el archivo Order_.cs_  (_Order.vb_). Decore la clase  **Order**  con los atributos  **DefaultClassOptions**  e  **ImageName**.
    

    
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
    

Puede agregar más código personalizado a las clases generadas automáticamente (por ejemplo, reemplazar métodos de clase base). No cambie el código ubicado en archivos con el sufijo del diseñador: contienen código generado por el  _diseñador_  y no deben modificarse manualmente. Las clases generadas se declaran como  _parciales_. Las piezas de clase diseñadas y personalizadas se encuentran en archivos diferentes.

>NOTA
Se recomienda especificar la [propiedad predeterminada](https://docs.devexpress.com/eXpressAppFramework/113525/business-model-design-orm/how-to-specify-a-display-member-for-a-lookup-editor-detail-form-caption) para cada clase generada a través del **sistema.Modelo de componentes.  Atributo de propiedad predeterminada**. La propiedad predeterminada se trata como un identificador legible en la interfaz de usuario de una aplicación XAF.

>IMPORTANTE
No se pueden aplicar atributos a las propiedades del código de la clase parcial. En su lugar, utilice el diseñador (consulte la siguiente sección).

## Agregar atributos específicos de XAF en el diseñador

Como alternativa, puede utilizar el diseñador para aplicar atributos. Enfoque la clase o el campo requiere y especifique la configuración  **Atributos personalizados**  en la ventana  **Propiedades**.

![image](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/assets/126447472/ffc3ae19-07de-4320-b851-3efce3f4f50b)


## Especificar la cadena de conexión

Se genera una aplicación XAF vacía con una cadena de conexión predeterminada: . Debe cambiarlo para que la aplicación utilice la base de datos necesaria. Cambie la cadena de conexión en el archivo de configuración de la aplicación. Puede copiar la cadena de conexión válida del archivo  _MySolution.Module\app.config_  creado automáticamente.`Data Source=(localdb)\mssqllocaldb;Initial Catalog=MyApplication;Integrated Security=SSPI;Pooling=false`

## Ejecutar la aplicación

Ahora puede ejecutar las aplicaciones WinForms y ASP.NET formularios Web Forms para ver el resultado. Estas aplicaciones se basan completamente en el modelo de negocio generado para la base de datos heredada.



![image](https://github.com/jjcolumb/Generate-XPO-Business-Classes-for-Existing-Data-Tables/assets/126447472/d879074e-4f7e-4fd9-9d5d-efbaf2faa737)
