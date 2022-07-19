using System;
using System.Collections.Generic;
using System.IO;

namespace apiGenerator
{
    class Program
    {
        private static string getIEntity()
        {
            string importLines = "using System;\nusing System.Collections.Generic;\nusing System.Text;\n";
            string nameSpace = "namespace Core\n{\n\tpublic interface IEntity\n\t{\n\t}\n}";
            string IEntity = importLines + nameSpace;
            return IEntity;
        }
        private static string getIEntityRepository(string projectName)
        {
            string IEntityRepository = File.ReadAllText("BaseText\\IEntityRepository.txt");
            return IEntityRepository;
        }
        private static string getEfEntityRepositoryBase()
        {
            string EfEntityRepositoryBase = File.ReadAllText("BaseText\\EfEntityRepositoryBase.txt");
            return EfEntityRepositoryBase;
        }
        private static void generateCore(string projectName)
        {
            Directory.CreateDirectory(projectName + "\\" + "Core");
            string prj = File.ReadAllText("BaseText\\Core.txt");
            File.WriteAllText(projectName + "\\" + "Core\\Core.csproj", prj);
            File.WriteAllText(projectName + "//Core//IEntity.cs", getIEntity());
            Directory.CreateDirectory(projectName + "\\" + "Core" + "\\" + "DataAccess");
            File.WriteAllText(projectName + "//Core//DataAccess//IEntityRepository.cs", getIEntityRepository(projectName));
            Directory.CreateDirectory(projectName + "\\" + "Core" + "\\" + "DataAccess\\EntityFramework");
            File.WriteAllText(projectName + "\\" + "Core" + "\\" + "DataAccess\\EntityFramework\\" + "EfEntityRepositoryBase.cs", getEfEntityRepositoryBase());
        }
        private static void generateBusiness(string projectName, List<string> entityNames)
        {

            Directory.CreateDirectory(projectName + "\\" + "Business");
            string prj = File.ReadAllText("BaseText\\Business.txt");
            File.WriteAllText(projectName + "\\" + "Business\\Business.csproj", prj);
            Directory.CreateDirectory(projectName + "\\" + "Business\\Abstract");
            Directory.CreateDirectory(projectName + "\\" + "Business\\Concrete");
            string baseTxt = File.ReadAllText("BaseText\\Services.txt");
            for (int i = 0; i < entityNames.Count; i++)
            {
                string Name = entityNames[i];
                string name = Name.ToLower();
                string newTxt = baseTxt.Replace("#", Name);
                newTxt = newTxt.Replace("*", name);
                string fileName = "I" + entityNames[i] + "Service.cs";
                File.WriteAllText(projectName + "\\" + "Business\\Abstract\\"+fileName,newTxt);
            }
            baseTxt = File.ReadAllText("BaseText\\Managers.txt");
            for (int i = 0; i < entityNames.Count; i++)
            {
                string Name = entityNames[i];
                string name = Name.ToLower();
                string newTxt = baseTxt.Replace("#", Name);
                newTxt = newTxt.Replace("*", name);
                string fileName = entityNames[i] + "Manager.cs";
                File.WriteAllText(projectName + "\\" + "Business\\Concrete\\" + fileName, newTxt);
            }
        }
        private static void generateDataAccess(string projectName,string conString, List<string> entityNames)
        {
            Directory.CreateDirectory(projectName + "\\" + "DataAccess");
            string prj = File.ReadAllText("BaseText\\DataAccess.txt");
            File.WriteAllText(projectName + "\\" + "DataAccess\\DataAccess.csproj", prj);
            Directory.CreateDirectory(projectName + "\\" + "DataAccess\\Abstract");
            Directory.CreateDirectory(projectName + "\\" + "DataAccess\\Concrete");
            Directory.CreateDirectory(projectName + "\\" + "DataAccess\\Concrete\\EntityFramework");
            string baseTxt = File.ReadAllText("BaseText\\IDal.txt");
            for (int i = 0; i < entityNames.Count; i++)
            {
                string Name = entityNames[i];
                string name = Name.ToLower();
                string newTxt = baseTxt.Replace("#", Name);
                newTxt = newTxt.Replace("*", name);
                string fileName = "I"+entityNames[i] + "Dal.cs";
                File.WriteAllText(projectName + "\\" + "DataAccess\\Abstract\\" + fileName, newTxt);
            }
            baseTxt = File.ReadAllText("BaseText\\EfDal.txt");
            string ProjectName = "";
            for (int i = 0; i < entityNames.Count; i++)
            {
                string Name = entityNames[i];
                string name = Name.ToLower();
                string newTxt = baseTxt.Replace("#", Name);
                newTxt = newTxt.Replace("*", name);
                ProjectName = projectName.ToLower();
                char[] nameChars = ProjectName.ToCharArray();
                nameChars[0] = Convert.ToChar(nameChars[0].ToString().ToUpper());
                ProjectName = "";
                for (int j = 0; j < nameChars.Length; j++)
                {
                    ProjectName += nameChars[j];
                }
                newTxt = newTxt.Replace("$", "Context");
                string fileName = "Ef" + entityNames[i] + "Dal.cs";
                File.WriteAllText(projectName + "\\" + "DataAccess\\Concrete\\EntityFramework\\" + fileName, newTxt);
            }
            baseTxt = File.ReadAllText("BaseText\\Context.txt");
            string contexes = "";
            for (int i = 0; i < entityNames.Count; i++)
            {
                string Name = entityNames[i];
                string baseLine = "public DbSet<#> #s { get; set; }";
                baseLine = baseLine.Replace("#", Name);
                contexes += baseLine + "\n\t\t";
            }

            baseTxt = baseTxt.Replace("#", contexes);
            baseTxt = baseTxt.Replace("*", conString);
            string fname = ProjectName + "Context.cs";
            baseTxt = baseTxt.Replace("$", "Context");
            File.WriteAllText(projectName + "\\" + "DataAccess\\Concrete\\EntityFramework\\Context.cs", baseTxt);

        }
        private static void generateEntities(string projectName, List<Entity> entities)
        {
            Directory.CreateDirectory(projectName + "\\" + "Entities");
            string prj = File.ReadAllText("BaseText\\EntityProject.txt");
            File.WriteAllText(projectName + "\\" + "Entities\\Entities.csproj",prj);
            Directory.CreateDirectory(projectName + "\\" + "Entities\\Concrete");
            string baseTxt = File.ReadAllText("BaseText\\Entities.txt");
            string parameterLine = "";
            string parameters = "";
            for (int i = 0; i < entities.Count; i++)
            {
                baseTxt = File.ReadAllText("BaseText\\Entities.txt");
                string Name = entities[i].Name;
                baseTxt = baseTxt.Replace("#", Name);
                parameterLine = "";
                for (int j = 0; j < entities[i].Attributes.Count; j++)
                {
                    string attribute = entities[i].Attributes[j].Name;
                    string type = entities[i].Attributes[j].Type;
                    parameterLine += "public " + type + " " + attribute + " { get; set; }\n\t\t";
                }

                Name = Name + ".cs";
                baseTxt = baseTxt.Replace("*", parameterLine);
                File.WriteAllText(projectName + "\\" + "Entities\\Concrete\\"+Name, baseTxt);
                
            }
            baseTxt = baseTxt.Replace("*", parameterLine);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Proje adini giriniz:");
            string projectName = Console.ReadLine();
            Directory.CreateDirectory(projectName);
            generateCore(projectName);
            Console.WriteLine("\n-----------------------------------Entity adlarını giriniz ve en son '#' giriniz-----------------------------------\n");
            string entityName = "";
            List<string> entityNames = new List<string>();
            List<Entity> entities = new List<Entity>();
            while (true)
            {
                Console.Write("Entity adı:");
                Entity entity = new Entity();
                entity.Attributes = new List<Attribute>();
                entityName = Console.ReadLine();
                if (entityName!="#"&&entityName!="")
                {
                    entityName = entityName.ToLower();
                    char[] nameChars = entityName.ToCharArray();
                    nameChars[0] = Convert.ToChar(nameChars[0].ToString().ToUpper());
                    entityName = "";
                    for (int i = 0; i < nameChars.Length; i++)
                    {
                        entityName += nameChars[i];
                    }
                    entityNames.Add(entityName);
                    
                    entity.Name = entityName;
                }
                else
                {
                    break;
                }
                Console.WriteLine("\n--------------------------Entity özelliklerini 'degisken_tipi degisken_adi' seklinde araya bosluk birakarak giriniz:\n" +
                                  "--------------------------özellikler bitiminde '*' giriniz:---------------------------\n");
                string attributeLine = "";
                while (true)
                {
                    Console.Write("degisken_tipi degisken_adi:");
                    attributeLine = Console.ReadLine();
                    if (attributeLine != "*"&&attributeLine !="")
                    {
                        string attributeName = attributeLine.Split(' ')[1];
                        string attributeType = attributeLine.Split(' ')[0];
                        attributeName = attributeName.ToLower();
                        char[] nameChars = attributeName.ToCharArray();
                        nameChars[0] = Convert.ToChar(nameChars[0].ToString().ToUpper());
                        attributeName = "";
                        for (int i = 0; i < nameChars.Length; i++)
                        {
                            attributeName += nameChars[i];
                        }

                        Attribute attribute = new Attribute();
                        attribute.Name = attributeName;
                        attribute.Type = attributeType;
                        entity.Attributes.Add(attribute);
                    }
                    else
                    {
                        break;
                    }
                }
                entities.Add(entity);
            }
            generateBusiness(projectName,entityNames);
            Console.WriteLine("Connection string'i giriniz:");
            string connectionString = Console.ReadLine();
            generateDataAccess(projectName,connectionString,entityNames);
            generateEntities(projectName,entities);

        }
    }
}
