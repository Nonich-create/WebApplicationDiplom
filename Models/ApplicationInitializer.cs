using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
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
                    },
                      new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Частное учреждение образования Белорусский институт правоведения"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Харьковский электротехнический техникум"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "ФПК ИПК при Гомельском кооперативном институте"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Факультет повышения квалификации руководящих работников и специалистов по бух. учету и финансам"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Факультет повышения квалификации ИПК при ГКИ по обеспечению сохранности собственности"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Факультет повышения квалификации ИПК при ГКИ"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Учреждение образования Республиканский институт профессионального образования"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Учреждение образования БГИПК по стандартизации, метрологии и у3правлению качеством"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Учебно-образовательное учреждение БГУ Республиканский институт высшей школы"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "УО Республиканский институт профессионального образования"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "УО Молодечненский торгово-экономический колледж Белкоопсоюза"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "УО Молодечненский государственный политехнический профессиональный лицей"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "УО Минский государственный лингвистический университет"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "УО Белорусский государственный экономический университет"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "УО Белорусский государственный педагогический университет имени Максима Танка"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "УНК ПК МКИ Центросоюза"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Техникум Молодечненского учебно-производственного комплекса ПТУ-техникум"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Спецфакультет психолодической переподготовки УО БГЭУ"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Республиканский институт профессионального образования"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Республиканский институт инновационных технологий	"
                    },  new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Республиканский институт высшей школы БГУ"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Профессионально-техническое училище №21 им. Ленинского комсомола"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Пинское педагогическое училище имени А.С. Пушкина"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Пинский учетно-кредитный техникум	"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Ошмянский государственный сельскохозяйственный техникум"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "ОАО Универсалпромстрой"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "НО учреждение Современная гуманитарная академия"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Московский орден Дружбы народов кооперативного института Центросоюза"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Московский новый юридический институт"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Молодечненское среднее ПТУ-21"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Молодечненское профессионально-техническое училище №87"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Молодечненский учетно-плановый техникум Белкоопсоюза"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Молодечненский торгово-экономический колледж"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Молодечненский техникум учебно-производственного комплекса ПТУ-техникум"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Могилевский государственный педагогический институт имени А. Кулешова"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Мінскі абласны УПК і ПКР і СА"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Минский институт управления"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Минский институт культуры"
                    }, new EducationalInstitutions
                    {
                        NameEducationalInstitutions = "Белорусский государственный институт повышения квалификации и переподготовки кадров"
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
                        Qualification = "Информационные системы и технологии (в экономике)	"
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
                IdentityResult result1 = await userManager.CreateAsync(userBKS, "1234567890");
                if (result1.Succeeded)
                {
                 
                    await userManager.AddToRoleAsync(userBKS, "user");
                    //    TableOrganizations organizations = new TableOrganizations
                    //{
                    //    NameOfOrganization = "белкоопсоюз",
                    //    UserId = userBKS.Id,
                    //    TypeOrganization = "Главный офис",
                    //    Email = "",
                    //    SubordinationId = 0,

                    //};
                    //context.TableOrganizations.Add(organizations);

                }
                //User admin = new User { Identifier = "admin",UserName = "admin" };
                //IdentityResult result2 = await userManager.CreateAsync(admin, "admin");
                //if (result2.Succeeded)
                //{
                //    await userManager.AddToRoleAsync(admin, "user");
                //}
                //if (!context.TableOrganizations.Any())
                //{

                //   await context.TableOrganizations.AddRangeAsync(new List<TableOrganizations>
                //   {
                //    new TableOrganizations
                //    {
                //        UserId = userBKS.Id,
                //        NameOfOrganization = "",
                //        SubordinationId = 0,
                //        TypeOrganization = "Главный офис"
                //    },
                //          new TableOrganizations
                //    {
                //        UserId =  admin.Id,
                //        NameOfOrganization = "",
                //        SubordinationId = 0,
                //    }
                //   });
                //}

            }
            
            

            await context.SaveChangesAsync();
        }

    }
}
 

 