using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebApplicationDiplom.Models
{
    public static class ApplicationInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationContext context)
        {
            if(!context.EducationalInstitutions.Any())
            {
                await context.EducationalInstitutions.AddRangeAsync(new List<EducationalInstitutions>
                {
                    new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Частное учреждение образования Минский институт управления"
                        ,Abbreviation ="ЧУОМиу"
                    },
                      new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Частное учреждение образования Белорусский институт правоведения"
                           ,Abbreviation ="ЧУОБИП"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Харьковский электротехнический техникум"
                           ,Abbreviation ="ХЭТ"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "ФПК ИПК при Гомельском кооперативном институте"
                           ,Abbreviation ="ФПКИПК"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Учреждение образования Республиканский институт профессионального образования"
                           ,Abbreviation ="УОРИПО"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Учреждение образования БГИПК по стандартизации, метрологии и у3правлению качеством"
                           ,Abbreviation ="УОБГИПК"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "УО Республиканский институт профессионального образования"
                           ,Abbreviation ="УОРИПО"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "УО Молодечненский торгово-экономический колледж Белкоопсоюза"
                           ,Abbreviation ="УОМТЭКБ"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "УО Минский государственный лингвистический университет"
                           ,Abbreviation ="УОМГЛУ"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "УО Белорусский государственный экономический университет"
                           ,Abbreviation ="УОБГЭУ"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "УО Белорусский государственный педагогический университет имени Максима Танка"
                           ,Abbreviation ="УОБГПУ"
                    },  new EducationalInstitutions                
                    {
                        NameEducationalInstitutions = "Минский институт управления"
                           ,Abbreviation ="МИУ"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Минский институт культуры"
                           ,Abbreviation ="МИК"
                    }
                }
                );
            }
            if(!context.Position.Any())
            {
                await context.Position.AddRangeAsync(new List<Position> {
                      new Position
                      {
                          JobTitle = "Председатель правления"
                      },
                      new Position
                      {
                          JobTitle = "Первый заместитель председателя правления"
                      },
                        new Position
                      {
                          JobTitle = "И.о.председателя правления"
                      },
                          new Position
                      {
                          JobTitle = "Председатель ревизионной комиссии"
                      },
                            new Position
                      {
                          JobTitle = "Главный бухгалтер райпо"
                      }
                            ,
                            new Position
                      {
                          JobTitle = "Директор филиала"
                      }
                                 ,
                            new Position
                      {
                          JobTitle = "И.о.директора"
                      }
                                 ,
                            new Position
                      {
                          JobTitle = "И.о.председателя ревизионной комиссии"
                      }
                                 ,
                            new Position
                      {
                          JobTitle = "Директор"
                      }
                                          ,
                            new Position
                      {
                          JobTitle = "И.о.директора филиала"
                      }
                                                       ,
                            new Position
                      {
                          JobTitle = "Председатель правления облпотребсоюза"
                      }                                     ,
                            new Position
                      {
                          JobTitle = "Председатель правления облпотребобщества"
                      }
                              ,
                            new Position
                      {
                          JobTitle = "Заместитель директора по торговле и общественному питанию"
                      }
                });
            }
            if(!context.TableQualification.Any())
            {
                await context.TableQualification.AddRangeAsync(new List<TableQualification>
                {
                    new TableQualification
                    {
                        Qualification = "Банковское дело"
                    },      new TableQualification
                    {
                        Qualification = "Управления организацией"
                    },      new TableQualification
                    {
                        Qualification = "Информационные системы и технологии"
                    },      new TableQualification
                    {
                        Qualification = "Информационные системы и технологии (в экономике)"
                    },      new TableQualification
                    {
                        Qualification = "Экономика и организация производства"
                    },      new TableQualification
                    {
                        Qualification = "Экономика и управление на предприятии"
                    },      new TableQualification
                    {
                        Qualification = "Юриспруденция"
                    }
                });
            }
            if (!context.TableArea.Any())
            {
                await context.TableArea.AddRangeAsync(new List<TableArea>
                {
                    new TableArea
                    {
                        NameArea = "Бретская"
                    },      new TableArea
                    {
                        NameArea = "Гомельская"
                    },      new TableArea
                    {
                        NameArea = "Гродненская"
                    },      new TableArea
                    {
                        NameArea = "Минская"
                    },      new TableArea
                    {
                        NameArea = "Могилевская"
                    },      new TableArea
                    {
                        NameArea = "Витебская"
                    }
                });
            }

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            if (!userManager.Users.Any())
            {
                User userBKS = new User {Identifier = "095126613431253",UserName = "095126613431253" };
                IdentityResult result1 = await userManager.CreateAsync(userBKS, "dY2-AJX-mJd-zuL");
                if (result1.Succeeded)
                {
                 
                    await userManager.AddToRoleAsync(userBKS, "user");
                    TableOrganizations organizations = new TableOrganizations
                    {
                        NameOfOrganization = "Белкоопсоюз",
                        UserId = userBKS.Id,
                        TypeOrganization = "Белкоопсоюз",
                        Email = "",
                        SubordinationId = 0,

                    };
                    context.TableOrganizations.Add(organizations);

                }
                User admin = new User { Identifier = "admin", UserName = "admin" };
                IdentityResult result2 = await userManager.CreateAsync(admin, "dY2-AJX-mJd-zuL");
                if (result2.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");   
                }
            }
            await context.SaveChangesAsync();
        }

    }
}
 

 