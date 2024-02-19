
using System.Text;

namespace CleanCode.Tools;

public class Program
{
    #region Constants
    const string sDonotDelete = "//<Do not Delete this line>";
    static readonly string ExePath = Directory.GetCurrentDirectory();
    // remove other path from name
    static readonly string SrcFolder = ExePath.Replace(@"CleanCode.Tools\bin\Debug\net6.0", "");
    static readonly string ToolsTemplateFolder = Path.Combine(SrcFolder, "CleanCode.Tools", "Template");
    static readonly string CoreEntitiesFolder = Path.Combine(SrcFolder, "CleanCode.Core", "Entities");
    static readonly string BusinessModelFolder = Path.Combine(SrcFolder, "CleanCode.Business", "Models");
    static readonly string BusinessInterfaceFolder = Path.Combine(SrcFolder, "CleanCode.Business", "Interfaces");
    static readonly string BusinessServicesFolder = Path.Combine(SrcFolder, "CleanCode.Business", "Services");
    static readonly string ApiModelsFolder = Path.Combine(SrcFolder, "CleanCode.Api", "Models");
    static readonly string ApiControllersFolder = Path.Combine(SrcFolder, "CleanCode.Api", "Controllers");

    public static bool ForceGenerate { get; set; }
    public static bool IgnoreViews { get; set; }


    #endregion
    public static void Main(string[] args)
    {
        //bool forceGenerate = false;
        if (args.Length > 0)
        {
            ForceGenerate = bool.Parse(args[0]);
            Console.WriteLine("ForceGenerate : " + ForceGenerate);
        }
        if (args.Length > 1)
        {
            IgnoreViews = bool.Parse(args[1]);
            Console.WriteLine("IgnoreViews : " + IgnoreViews);
        }

        AddBaseClassinEntities();
        CreateModelsInBusiness();
        if (ForceGenerate == false)
        {
            CreateInterfaceClassInBusiness();
            CreateServiceClassInBusiness();
            CreateModelsInApi();
            CreateControllersClassInApi();
        }

    }

    /// <summary>
    /// Add 'Entiry' base class in entity file of CleanCode.Core\Entities folder
    /// </summary>
    private static void AddBaseClassinEntities()
    {
        Console.WriteLine("Add Entity Base Class in CleanCode.Model started");
        //Get Model File list from Core project
        // remove other path from name
        if (Directory.Exists(CoreEntitiesFolder))
        {
            var fileList = Directory.GetFiles(CoreEntitiesFolder);
            foreach (var item in fileList)
            {
                FileInfo fileInfo = new(item);
                var sFromClassName = fileInfo.Name.Replace(".cs", "");
                if (fileInfo.Name == "BNMSContext.cs" || fileInfo.Name == "OrderInput.cs" || fileInfo.Name == "OrderResponse.cs")
                    continue;
                string input = string.Empty;
                using (StreamReader reader = new(item))
                {
                    input = reader.ReadToEnd();
                    reader.Close();
                }
                if (input.Contains(" : BaseEntity"))
                {
                    Console.WriteLine("Skip file : " + fileInfo.Name);
                    continue;
                }
                var sClassName = "public partial class " + sFromClassName;
                var output = "using CleanCode.Core.Entities.Base;";
                output += Environment.NewLine;
                output += input.Replace(sClassName, sClassName + " : BaseEntity");
                using (StreamWriter writer = new(item))
                {
                    writer.Write(output);
                    writer.Close();
                }
                Console.WriteLine("Base class BaseEntity added in file : " + fileInfo.Name);
            }
        }
        Console.WriteLine("Add Entity Base Class in CleanCode.Model ended" + Environment.NewLine);
    }
    /// <summary>
    /// Create Models In Business
    /// </summary>
    private static void CreateModelsInBusiness()
    {
        Console.WriteLine("Create Models In CleanCode.Business started");
        //Get Model File list from Core project
        string[] destExistingFileList = Directory.GetFiles(BusinessModelFolder);
        if (Directory.Exists(CoreEntitiesFolder))
        {
            var fileList = Directory.GetFiles(CoreEntitiesFolder);
            StringBuilder sbMapper = new();
            foreach (var fileName in fileList)
            {
                FileInfo fileInfo = new(fileName);
                var sFromClassName = fileInfo.Name.Replace(".cs", "");
                var sDestinationClassName = fileInfo.Name.Replace(".cs", "") + "Model";
                var sDestinationFileName = sFromClassName + "Model.cs";
                string sDestinationFullFileName = Path.Combine(BusinessModelFolder, sDestinationFileName);
                if (IgnoreFile(fileInfo.Name))
                {
                    continue;
                }
                if (!ForceGenerate)
                {
                    if (destExistingFileList.Contains(sDestinationFullFileName))
                    {
                        Console.WriteLine("Skip file : " + sDestinationFileName);
                        continue;
                    }
                }
                sbMapper.AppendLine("CreateMap<" + sFromClassName + ", " + sFromClassName + "Model>().ReverseMap();");
                string[] lines = File.ReadAllLines(fileInfo.FullName);
                StringBuilder sbProperties = new();
                if (lines.Length > 0)
                {
                    foreach (var line in lines)
                    {
                        if (string.IsNullOrEmpty(line))
                            continue;
                        if (line.Contains("public virtual"))
                            continue;
                        if (line.Contains("CreatedDate") || line.Contains("CreatedBy") || line.Contains("ModifiedDate") || line.Contains("ModifiedBy"))
                            continue;
                        if (line.Contains("get") && line.Contains("set"))
                        {
                            sbProperties.AppendLine("        " + line.Trim());
                        }
                    }
                }
                var sProperties = sbProperties.ToString().Trim();
                string sDestClassTemplate = BusinessModelClassTemplate();
                sDestClassTemplate = sDestClassTemplate.Replace("<<ModelFileName>>", sDestinationClassName);
                sDestClassTemplate = sDestClassTemplate.Replace("<<properties>>", sProperties);
                File.WriteAllText(sDestinationFullFileName, sDestClassTemplate);
                Console.WriteLine("File added : " + sDestinationFileName);
            }
            if (!ForceGenerate)
            {
                //Update Mapper class
                string sDestinationMapperFile = Path.Combine(SrcFolder, "CleanCode.Business", "Mapper", "ObjectMapper.cs");
                UpdateMapperclass(sDestinationMapperFile, sbMapper.ToString());
            }
        }
        Console.WriteLine("Create Models In CleanCode.Business ended" + Environment.NewLine);
    }

    private static bool IgnoreFile(string fileName)
    {
        if (ForceGenerate && IgnoreViews)
        {
            if (fileName.StartsWith("Vw") || fileName.StartsWith("LoadTest"))
            {
                return true;
            }
        }
        if (fileName == "BNMSContext.cs" || fileName == "OrderInput.cs" || fileName == "Metadata.cs" || fileName == "OrderResponse.cs")
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Create Interface Class In Business
    /// </summary>
    private static void CreateInterfaceClassInBusiness()
    {
        Console.WriteLine("Create Interface And Service Class In Business started");
        //Get Model File list from Core project
        string[] destExistingFileList = Directory.GetFiles(BusinessInterfaceFolder);
        if (Directory.Exists(CoreEntitiesFolder))
        {
            var fileList = Directory.GetFiles(CoreEntitiesFolder);
            foreach (var fileName in fileList)
            {
                FileInfo fileInfo = new(fileName);
                var sFromClassName = fileInfo.Name.Replace(".cs", "");
                var sDestinationFileName = "I" + sFromClassName + "Service.cs";
                string sDestinationFullFileName = Path.Combine(BusinessInterfaceFolder, sDestinationFileName);
                if (IgnoreFile(fileInfo.Name))
                {
                    continue;
                }

                if (!ForceGenerate)
                {
                    if (destExistingFileList.Contains(sDestinationFullFileName))
                    {
                        Console.WriteLine("Skip file : " + sDestinationFileName);
                        continue;
                    }
                }
                string sDestClassTemplate = BusinessInterfaceClassTemplate();
                sDestClassTemplate = sDestClassTemplate.Replace("<entity>", sFromClassName);
                sDestClassTemplate = sDestClassTemplate.Replace("<lentity>", sFromClassName.ToLower());
                File.WriteAllText(sDestinationFullFileName, sDestClassTemplate);
                Console.WriteLine("File added : " + sDestinationFileName);
            }
        }
        Console.WriteLine("Create Interface And Service Class In Business ended" + Environment.NewLine);
    }
    /// <summary>
    /// Business Interface Class Template
    /// </summary>
    /// <returns></returns>
    private static string BusinessInterfaceClassTemplate()
    {
        string input = string.Empty;
        var sControllerTemplate = Path.Combine(ToolsTemplateFolder, "Interface.txt");
        using (StreamReader reader = new(sControllerTemplate))
        {
            input = reader.ReadToEnd();
            reader.Close();
        }
        return input;
    }
    /// <summary>
    /// Create Service Class In Business
    /// </summary>
    private static void CreateServiceClassInBusiness()
    {
        Console.WriteLine("Create Interface And Service Class In Business started");
        //Get Model File list from Core project
        string[] destExistingFileList = Directory.GetFiles(BusinessServicesFolder);
        if (Directory.Exists(CoreEntitiesFolder))
        {
            var fileList = Directory.GetFiles(CoreEntitiesFolder);
            foreach (var fileName in fileList)
            {
                FileInfo fileInfo = new(fileName);
                var sFromClassName = fileInfo.Name.Replace(".cs", "");
                var sDestinationFileName = sFromClassName + "Service.cs";
                string sDestinationFullFileName = Path.Combine(BusinessServicesFolder, sDestinationFileName);
                if (IgnoreFile(fileInfo.Name))
                {
                    continue;
                }
                if (!ForceGenerate)
                {
                    if (destExistingFileList.Contains(sDestinationFullFileName))
                    {
                        Console.WriteLine("Skip file : " + sDestinationFileName);
                        continue;
                    }
                }
                string sDestClassTemplate = BusinessServiceClassTemplate();
                sDestClassTemplate = sDestClassTemplate.Replace("<Entity>", sFromClassName);
                sDestClassTemplate = sDestClassTemplate.Replace("<lEntity>", sFromClassName.ToLower());
                File.WriteAllText(sDestinationFullFileName, sDestClassTemplate);
                Console.WriteLine("File added : " + sDestinationFileName);
            }
        }
        Console.WriteLine("Create Interface And Service Class In Business ended" + Environment.NewLine);
    }
    /// <summary>
    /// BusinessServiceClassTemplate
    /// </summary>
    /// <returns></returns>
    private static string BusinessServiceClassTemplate()
    {
        string input = string.Empty;
        var sServiceTemplate = Path.Combine(ToolsTemplateFolder, "Service.txt");
        using (StreamReader reader = new(sServiceTemplate))
        {
            input = reader.ReadToEnd();
            reader.Close();
        }
        return input;
    }
    /// <summary>
    /// Business Model Class Template
    /// </summary>
    /// <returns></returns>
    private static string BusinessModelClassTemplate()
    {
        StringBuilder sb = new();
        sb.AppendLine("using CleanCode.Business.Models.Base;");
        sb.AppendLine("");
        sb.AppendLine("namespace CleanCode.Business.Models");
        sb.AppendLine("{");
        sb.AppendLine("    public class <<ModelFileName>> : BaseModel");
        sb.AppendLine("    {");
        sb.AppendLine("        <<properties>>");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        return sb.ToString();
    }
    /// <summary>
    /// Create Models In CleanCode.Api
    /// </summary>
    private static void CreateModelsInApi()
    {
        try
        {
            Console.WriteLine("Create Models In CleanCode.Api started");
            //Get Model File list from Core project
            string[] destExistingFileList = Directory.GetFiles(ApiModelsFolder);
            if (Directory.Exists(CoreEntitiesFolder))
            {
                var fileList = Directory.GetFiles(CoreEntitiesFolder);
                StringBuilder sbMapper = new();
                foreach (var fileName in fileList)
                {
                    FileInfo fileInfo = new(fileName);
                    var sFromClassName = fileInfo.Name.Replace(".cs", "");
                    var sDestinationClassName = fileInfo.Name.Replace(".cs", "") + "ApiModel";
                    var sDestinationFileName = sFromClassName + "ApiModel.cs";
                    string sDestinationFullFileName = Path.Combine(ApiModelsFolder, sDestinationFileName);
                    if (IgnoreFile(fileInfo.Name))
                    {
                        continue;
                    }
                    if (!ForceGenerate)
                    {
                        if (destExistingFileList.Contains(sDestinationFullFileName))
                        {
                            Console.WriteLine("Skip file : " + sDestinationFileName);
                            continue;
                        }
                    }
                    sbMapper.AppendLine("CreateMap<" + sFromClassName + "Model, " + sFromClassName + "ApiModel>().ReverseMap();");
                    string[] lines = File.ReadAllLines(fileInfo.FullName);
                    StringBuilder sbProperties = new();
                    if (lines.Length > 0)
                    {
                        foreach (var line in lines)
                        {
                            if (string.IsNullOrEmpty(line))
                                continue;
                            if (line.Contains("public virtual"))
                                continue;
                            if (line.Contains("CreatedDate") || line.Contains("CreatedBy") || line.Contains("ModifiedDate") || line.Contains("ModifiedBy"))
                                continue;
                            if (line.Contains("get") && line.Contains("set"))
                            {
                                sbProperties.AppendLine("        " + line.Trim());
                            }
                        }
                    }
                    var sProperties = sbProperties.ToString().Trim();
                    string sDestClassTemplate = ApiModelClassTemplate();
                    sDestClassTemplate = sDestClassTemplate.Replace("<<ModelFileName>>", sDestinationClassName);
                    sDestClassTemplate = sDestClassTemplate.Replace("<<properties>>", sProperties);
                    File.WriteAllText(sDestinationFullFileName, sDestClassTemplate);
                    Console.WriteLine("File added: " + sDestinationFileName);
                }
                if (!ForceGenerate)
                {
                    //Update Mapper class
                    string sDestinationMapperFile = Path.Combine(SrcFolder, "CleanCode.Api", "Mapper", "BNMSProfile.cs");
                    UpdateMapperclass(sDestinationMapperFile, sbMapper.ToString());
                }
            }
            Console.WriteLine("Create Models In CleanCode.Api ended" + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// ApiModelClassTemplate
    /// </summary>
    /// <returns></returns>
    private static string ApiModelClassTemplate()
    {
        StringBuilder sb = new();
        sb.AppendLine("using System.ComponentModel.DataAnnotations;");
        sb.AppendLine("");
        sb.AppendLine("namespace CleanCode.Api.Models");
        sb.AppendLine("{");
        sb.AppendLine("    public class <<ModelFileName>> : BaseApiModel");
        sb.AppendLine("    {");
        sb.AppendLine("        <<properties>>");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        return sb.ToString();
    }
    /// <summary>
    /// Update content in Mapper class
    /// </summary>
    /// <param name="sBusinessMapperFolder"></param>
    /// <param name="sNewMapperContent"></param>
    private static void UpdateMapperclass(string sBusinessMapperFolder, string sNewMapperContent)
    {
        if (!string.IsNullOrEmpty(sNewMapperContent))
        {
            string sMapperFileContent = string.Empty;
            using (StreamReader reader = new(sBusinessMapperFolder))
            {
                sMapperFileContent = reader.ReadToEnd();
                reader.Close();
            }

            if (sMapperFileContent.Contains(sDonotDelete))
            {
                var sReplaceContent = string.Join(string.Empty, sNewMapperContent, Environment.NewLine, sDonotDelete);
                sMapperFileContent = sMapperFileContent.Replace(sDonotDelete, sReplaceContent);
            }
            using (StreamWriter writer = new(sBusinessMapperFolder))
            {
                writer.Write(sMapperFileContent);
                writer.Close();
            }
        }
    }
    /// <summary>
    /// Create Service Class In Business
    /// </summary>
    private static void CreateControllersClassInApi()
    {
        Console.WriteLine("Create Controllers Class In CleanCode.Api started");
        //Get Model File list from Core project
        string[] destExistingFileList = Directory.GetFiles(ApiControllersFolder);
        if (Directory.Exists(CoreEntitiesFolder))
        {
            var fileList = Directory.GetFiles(CoreEntitiesFolder);
            StringBuilder sbMapper = new();
            foreach (var fileName in fileList)
            {
                FileInfo fileInfo = new(fileName);
                var sFromClassName = fileInfo.Name.Replace(".cs", "");
                var sDestinationFileName = sFromClassName + "Controller.cs";
                string sDestinationFullFileName = Path.Combine(ApiControllersFolder, sDestinationFileName);
                if (IgnoreFile(fileInfo.Name))
                {
                    continue;
                }

                if (!ForceGenerate)
                {
                    if (destExistingFileList.Contains(sDestinationFullFileName))
                    {
                        Console.WriteLine("Skip file : " + sDestinationFileName);
                        continue;
                    }
                }
                sbMapper.AppendLine("services.AddScoped<I" + sFromClassName + "Service, " + sFromClassName + "Service>();");
                string sDestClassTemplate = ControllersClassTemplate();
                sDestClassTemplate = sDestClassTemplate.Replace("<Entity>", sFromClassName);
                sDestClassTemplate = sDestClassTemplate.Replace("<lEntity>", sFromClassName.ToLower());
                File.WriteAllText(sDestinationFullFileName, sDestClassTemplate);
                Console.WriteLine("File added : " + sDestinationFileName);
            }
            if (!ForceGenerate)
            {
                //Update Mapper class
                string sDestinationMapperFile = Path.Combine(SrcFolder, "CleanCode.Api", "Extensions", "ServiceExtensions.cs");
                UpdateMapperclass(sDestinationMapperFile, sbMapper.ToString());
            }
        }
        Console.WriteLine("Create Controllers Class In CleanCode.Api ended" + Environment.NewLine);
    }
    /// <summary>
    /// BusinessServiceClassTemplate
    /// </summary>
    /// <returns></returns>
    private static string ControllersClassTemplate()
    {
        string input = string.Empty;
        var sControllerTemplate = Path.Combine(ToolsTemplateFolder, "Controller.txt");
        using (StreamReader reader = new(sControllerTemplate))
        {
            input = reader.ReadToEnd();
            reader.Close();
        }
        return input;
    }
}
