using BoatDataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatDataAccess
{
    public class BoatAccessor
    {
        public static List<Boat> RetrieveBoatsByStatus(string boatStatusID)
        {
            var boats = new List<Boat>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_boats_by_status";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@BoatStatusID", SqlDbType.VarChar, 50);
            cmd.Parameters["@BoatStatusID"].Value = boatStatusID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // BoatID, BoatTypeID, Name, Hours, ModelYear, PurchaseDate,
                        // BoatStatusID, DockSlipID, Color
                        boats.Add(new Boat()
                        {
                            BoatID = reader.GetInt32(0),
                            BoatTypeID = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Hours = reader.GetInt32(3),
                            ModelYear = reader.GetInt32(4),
                            PurchaseDate = reader.GetDateTime(5),
                            BoatStatusID = reader.GetString(6),
                            DockSlipID = reader.GetInt32(7),
                            Color = reader.GetString(8),
                            Active = true
                        });
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return boats;
        }

        public static List<Boat> RetrieveBoatsByActive(bool active)
        {
            var boats = new List<Boat>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_boats_by_active";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = active;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // 		SELECT BoatID, BoatTypeID, Name, Hours, ModelYear, PurchaseDate,
                        //      BoatStatusID, DockSlipID, Color, Active
                        boats.Add(new Boat()
                        {
                            BoatID = reader.GetInt32(0),
                            BoatTypeID = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Hours = reader.GetInt32(3),
                            ModelYear = reader.GetInt32(4),
                            PurchaseDate = reader.GetDateTime(5),
                            BoatStatusID = reader.GetString(6),
                            DockSlipID = reader.GetInt32(7),
                            Color = reader.GetString(8),
                            Active = reader.GetBoolean(9)
                        });
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return boats;
        }

        public static int UpdateBoatStatus(int boatID, string oldBoatStatusID,
                                            string newBoatStatusID)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_update_boatstatus";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BoatID", SqlDbType.Int);
            cmd.Parameters.Add("@OldBoatStatusID", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@NewBoatStatusID", SqlDbType.VarChar, 50);

            cmd.Parameters["@BoatID"].Value = boatID;
            cmd.Parameters["@OldBoatStatusID"].Value = oldBoatStatusID;
            cmd.Parameters["@NewBoatStatusID"].Value = newBoatStatusID;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public static int CreateNewBoat( Boat boat)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_insert_boat";

            var cmd = new SqlCommand(cmdText, conn) {CommandType = CommandType.StoredProcedure};

            cmd.Parameters.AddWithValue("@BoatTypeID", boat.BoatTypeID);
            cmd.Parameters.AddWithValue("@Name", boat.Name);
            cmd.Parameters.AddWithValue("@Hours", boat.Hours);
            cmd.Parameters.AddWithValue("ModelYear", boat.ModelYear);
            cmd.Parameters.AddWithValue("@PurchaseDate", boat.PurchaseDate);
            cmd.Parameters.AddWithValue("@BoatStatusID", boat.BoatStatusID);
            cmd.Parameters.AddWithValue("@DockSlipID", boat.DockSlipID);
            cmd.Parameters.AddWithValue("@Color", boat.Color);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        public static int UpdateBoatDetail(Boat oldBoat, Boat newBoat)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_update_boat";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@OldBoatTypeID", oldBoat.BoatTypeID);
            cmd.Parameters.AddWithValue("@OldName", oldBoat.Name);
            cmd.Parameters.AddWithValue("@OldHours", oldBoat.Hours);
            cmd.Parameters.AddWithValue("OldModelYear", oldBoat.ModelYear);
            cmd.Parameters.AddWithValue("@OldPurchaseDate", oldBoat.PurchaseDate);
            cmd.Parameters.AddWithValue("@OldBoatStatusID", oldBoat.BoatStatusID);
            cmd.Parameters.AddWithValue("@OldDockSlipID", oldBoat.DockSlipID);
            cmd.Parameters.AddWithValue("@OldColor", oldBoat.Color);

            cmd.Parameters.AddWithValue("@NewBoatTypeID", newBoat.BoatTypeID);
            cmd.Parameters.AddWithValue("@NewName", newBoat.Name);
            cmd.Parameters.AddWithValue("@NewHours", newBoat.Hours);
            cmd.Parameters.AddWithValue("NewModelYear", newBoat.ModelYear);
            cmd.Parameters.AddWithValue("@NewPurchaseDate", newBoat.PurchaseDate);
            cmd.Parameters.AddWithValue("@NewBoatStatusID", newBoat.BoatStatusID);
            cmd.Parameters.AddWithValue("@NewDockSlipID", newBoat.DockSlipID);
            cmd.Parameters.AddWithValue("@NewColor", newBoat.Color);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }
    }
}
