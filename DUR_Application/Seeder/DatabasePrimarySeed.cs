using DUR_Application.Entities;
using DUR_Application.Helper;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace DUR_Application.Seeder
{
    public abstract class DatabasePrimarySeed
    {
        protected IEnumerable<Role> GetRoles() //Roles
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "Admin",
                },
                new Role()
                {
                    Name = "Manager",
                },
                new Role()
                {
                    Name = "Lider",
                },
                new Role()
                {
                    Name = "ProductionLeader",
                },
                new Role()
                {
                    Name = "Mechanic",
                },
                new Role()
                {
                    Name = "LaneWorker",
                },
            };
        
            return roles;
        }

        protected IEnumerable<Lane> GetLanes() //Lanes
        {
            var lanes = new List<Lane>()
            {
                new Lane()
                {
                    Number = "13730002",
                    Describiton = "SPR4",
                    Machines = new List<Machine>()
                    {
                        new Machine()
                        {
                            MachineName = "MODUŁ01",
                            MachineDescription = "Moduł 01",
                        },
                        new Machine()
                        {
                            MachineName = "MODUŁ02",
                            MachineDescription = "Moduł 02",
                        },new Machine()
                        {
                            MachineName = "MODUŁ03",
                            MachineDescription = "Moduł 03",
                        },new Machine()
                        {
                            MachineName = "MODUŁ03",
                            MachineDescription = "Moduł 03",
                        },new Machine()
                        {
                            MachineName = "MODUŁ04",
                            MachineDescription = "Moduł 04",
                        },new Machine()
                        {
                            MachineName = "MODUŁ05",
                            MachineDescription = "Moduł 05",
                        },new Machine()
                        {
                            MachineName = "MODUŁ06",
                            MachineDescription = "Moduł 06",
                        },new Machine()
                        {
                            MachineName = "MODUŁ07",
                            MachineDescription = "Moduł 07",
                        },
                    }
                },new Lane()
                {
                    Number = "13748002",
                    Describiton = "SPR4-Pasy",
                    Machines = new List<Machine>()
                    {
                        new Machine()
                        {
                            MachineName = "01_Szycie_pod_Kołek",
                            MachineDescription = "Szycie pod Kołek",
                        },
                        new Machine()
                        {
                            MachineName = "02_Zaciąganie",
                            MachineDescription = "Zaciąganie",
                        },new Machine()
                        {
                            MachineName = "03_Kontrola_ATOS",
                            MachineDescription = "ATOS",
                        },new Machine()
                        {
                            MachineName = "04_Szycie_pod_Loopik",
                            MachineDescription = "Szycie pod Loopik",
                        },new Machine()
                        {
                            MachineName = "05_Szycie_pod_zaczep",
                            MachineDescription = "Szycie pod zaczep",
                        },new Machine()
                        {
                            MachineName = "06_Kontrola_wizualna",
                            MachineDescription = "Kontrola Wizualna",
                        }
                        
                    }
                },
            };

            return lanes;
        }

        protected User GetAdmin()
        {
            var passwordHassher = new PasswordHasher<User>();

            var admin = new User()
            {
                Email = "admin@admin.com",
                FirstName = "admin",
                LastName = "admin",
                UserCode = GenerateRandomCode.GenerateCode(),
                RoleId = 1
            };

            var hashPasword = passwordHassher.HashPassword(admin, "admin");
            admin.Password = hashPasword;
            admin.ConfirmPassword = hashPasword;

            return admin;
        } //Users

        protected Magazine GetMagazine() //Magazine
        {

            var magazine = new Magazine()
            {
                Parts = new List<SparePart>()
                {
                    new SparePart()
                    {
                        Name = "Szpulka",
                        Type = "Maszyny Szyjace",
                        Description = "Szpulka",
                        Price = 10.50m
                    },new SparePart()
                    {
                        Name = "KEYENCE SR-710",
                        Type = "Skaner",
                        Description = "Skaner Keyence",
                        Price = 1890m
                    }
                }
            };



            return magazine;
        }

        protected IEnumerable<RequestType> GetRequestTypes()
        {

            var requestType = new List<RequestType>()
            {
                new RequestType()
                {
                    Name = "Regulacja"
                }, 
                new RequestType()
                {
                    Name = "Awaria"
                }, 
                new RequestType()
                {
                    Name = "Działanie Prewencyjne"
                },
                new RequestType()
                {
                    Name = "PTU"
                },
                new RequestType()
                {
                    Name = "Regulacja po przezbrojeniu"
                },
                new RequestType()
                {
                    Name = "Regulacja po komponent"
                }, 
                new RequestType()
                {
                    Name = "Mikroprzestój"
                },
            };


            return requestType; 

        } 

        protected IEnumerable<RequestStatus> GetRequestStatus()
        {
            var requestType = new List<RequestStatus>()
            {
                new RequestStatus()
                {
                    Status = "Open"
                },
                new RequestStatus()
                {
                    Status = "Pending"
                },
                new RequestStatus()
                {
                    Status = "Closed"
                }
            };

            return requestType;
        }
    }
}
