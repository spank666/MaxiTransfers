using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApp.BDD;
using WebApp.Dtos;

namespace WebApp.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly SqlServerDbContext _sqlServer;
        public BeneficiaryService(SqlServerDbContext sqlServer)
        {
            _sqlServer = sqlServer;
        }
        public async Task<List<BeneficiaryDto>> AllBeneficiariesOfEmployee(int id)
        {
            List<BeneficiaryDto> list = new List<BeneficiaryDto>();
            try
            {
                var parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@IdEmployee",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = id
                        }
                };
                using (var command = _sqlServer.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[dbo].[SPR_GET_ALL_BENEFICIARIES_OF_EMPLOYEE]";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(parameters);

                    await _sqlServer.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        while (result.Read())
                        {
                            var item = new BeneficiaryDto();
                            item.Id = Convert.ToInt32(result["Id"]);
                            item.Nombres = result["FirstName"].ToString();
                            item.Apellidos = result["LastName"].ToString();
                            item.Fecha = Convert.ToDateTime(result["DateOfBirth"]);
                            item.Porcentaje = Convert.ToInt32(result["ParticipationPercentage"]);
                            item.CURP = result["Curp"].ToString();
                            item.SSN = result["Ssn"].ToString();
                            item.Telefono = result["PhoneNumber"].ToString();
                            item.Nacionalidad = result["Nationality"].ToString();
                            list.Add(item);
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
        }

        public async Task<bool> Add(BeneficiaryDto beneficiary)
        {
            bool response = false;
            try
            {
                var parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@FirstName",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = beneficiary.Nombres
                        },
                        new SqlParameter() {
                            ParameterName = "@LastName",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = beneficiary.Apellidos
                        },
                        new SqlParameter() {
                            ParameterName = "@DateOfBirth",
                            SqlDbType =  System.Data.SqlDbType.DateTime,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = beneficiary.Fecha
                        },
                        //new SqlParameter() {
                        //    ParameterName = "@ParticipationPercentage",
                        //    SqlDbType =  System.Data.SqlDbType.Int,
                        //    Direction = System.Data.ParameterDirection.Input,
                        //    Value = beneficiary.Porcentaje
                        //},
                        new SqlParameter() {
                            ParameterName = "@Curp",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 18,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = beneficiary.CURP
                        },
                        new SqlParameter() {
                            ParameterName = "@Ssn",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 11,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = beneficiary.SSN
                        },
                        new SqlParameter() {
                            ParameterName = "@PhoneNumber",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 10,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = beneficiary.Telefono
                        },
                        new SqlParameter() {
                            ParameterName = "@Nationality",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = beneficiary.Nacionalidad
                        },
                        new SqlParameter() {
                            ParameterName = "@IdEmployee",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = beneficiary.IdEmployee
                        },
                        new SqlParameter() {
                            ParameterName = "@Id",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Output,
                        }
                };

                using (var command = _sqlServer.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[dbo].[SPR_ADD_BENEFICIARY]";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(parameters);

                    await _sqlServer.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {

                        response = Convert.ToInt32(parameters[8].Value) > 0;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            bool response = false;
            try
            {
                var parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@Id",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Id
                        },
                        new SqlParameter() {
                            ParameterName = "@HasError",
                            SqlDbType =  System.Data.SqlDbType.Bit,
                            Direction = System.Data.ParameterDirection.Output,
                        }
                };

                using (var command = _sqlServer.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[dbo].[SPR_DELETE_BENEFICIARY]";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(parameters);

                    await _sqlServer.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {

                        response = !Convert.ToBoolean(parameters[1].Value);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public async Task<bool> Update(BeneficiaryDto beneficiary)
        {
            bool response = false;
            try
            {
                var parameters = new SqlParameter[] {
                    new SqlParameter() {
                        ParameterName = "@Id",
                        SqlDbType =  System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = beneficiary.Id
                    },
                    new SqlParameter() {
                        ParameterName = "@FirstName",
                        SqlDbType =  System.Data.SqlDbType.VarChar,
                        Size = 100,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = beneficiary.Nombres
                    },
                    new SqlParameter() {
                        ParameterName = "@LastName",
                        SqlDbType =  System.Data.SqlDbType.VarChar,
                        Size = 100,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = beneficiary.Apellidos
                    },
                    new SqlParameter() {
                        ParameterName = "@DateOfBirth",
                        SqlDbType =  System.Data.SqlDbType.DateTime,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = beneficiary.Fecha
                    },
                    new SqlParameter() {
                        ParameterName = "@ParticipationPercentage",
                        SqlDbType =  System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = beneficiary.Porcentaje
                    },
                    new SqlParameter() {
                        ParameterName = "@Curp",
                        SqlDbType =  System.Data.SqlDbType.VarChar,
                        Size = 18,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = beneficiary.CURP
                    },
                    new SqlParameter() {
                        ParameterName = "@Ssn",
                        SqlDbType =  System.Data.SqlDbType.VarChar,
                        Size = 11,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = beneficiary.SSN
                    },
                    new SqlParameter() {
                        ParameterName = "@PhoneNumber",
                        SqlDbType =  System.Data.SqlDbType.VarChar,
                        Size = 10,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = beneficiary.Telefono
                    },
                    new SqlParameter() {
                        ParameterName = "@Nationality",
                        SqlDbType =  System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = beneficiary.Nacionalidad
                    },
                    new SqlParameter() {
                        ParameterName = "@HasError",
                        SqlDbType =  System.Data.SqlDbType.Bit,
                        Direction = System.Data.ParameterDirection.Output
                    }
                };

                using (var command = _sqlServer.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[dbo].[SPR_UPDATE_BENEFICIARY]";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(parameters);

                    await _sqlServer.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {

                        response = !Convert.ToBoolean(parameters[9].Value);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public async Task<bool> UpdatePercentage(List<BeneficiaryDto> items)
        {
            bool response = false;
            try
            {
                var myDataTable = ToDataTable(items);
                var parameters = new SqlParameter[] {
                    new SqlParameter
                    {
                        ParameterName = "@PercentageType",
                        SqlDbType = SqlDbType.Structured,
                        Value = myDataTable
                    },
                    new SqlParameter() {
                        ParameterName = "@HasError",
                        SqlDbType =  System.Data.SqlDbType.Bit,
                        Direction = System.Data.ParameterDirection.Output
                    }
                };

                using (var command = _sqlServer.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[dbo].[SPR_UPDATE_PERCENTAGE]";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(parameters);

                    await _sqlServer.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {

                        response = !Convert.ToBoolean(parameters[9].Value);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public DataTable ToDataTable(List<BeneficiaryDto> items)
        {
            var table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Percentage", typeof(int));
            foreach (var item in items)
            {
                var row = table.NewRow();
                row.SetField("Id", item.Id);
                row.SetField("Percentage", item.Porcentaje);
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
