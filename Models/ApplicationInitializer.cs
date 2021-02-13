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
        public static async Task InitializeAsync(ApplicationContext context)
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
                          JobTitle = "Работник облпотребсоюза"
                      },
                      new Position
                      {
                          JobTitle = "Председатель правления райпо"
                      },
                        new Position
                      {
                          JobTitle = "Председател  ревизионный комиссии  райпо"
                      },
                          new Position
                      {
                          JobTitle = "Заместитель председателя правления райпо"
                      },
                            new Position
                      {
                          JobTitle = "Главный бухгалтер райпо"
                      }
                            ,
                            new Position
                      {
                          JobTitle = "Директор филиала  "
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
            await context.SaveChangesAsync();
        }

    }
}
