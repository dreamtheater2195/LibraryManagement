using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace QuanLyThuVien
{
    public partial class clsMuonTraSach : clsDBInteractionBase
    {
        #region Class Member Declarations
        private SqlBoolean m_bTonTai;
        private SqlDateTime m_daNgayTra, m_daNgayTra_ThucTe, m_daNgayMuon;
        private SqlInt32 m_iID_MuonSach, m_iID_DocGia;
        #endregion


        public clsMuonTraSach()
        {
            // Nothing for now.
        }


        public override bool Insert()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_MuonTraSach_Insert]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = m_scoMainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ID_DocGia", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iID_DocGia));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@NgayMuon", SqlDbType.SmallDateTime, 3, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_daNgayMuon));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@NgayTra", SqlDbType.SmallDateTime, 3, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_daNgayTra));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@NgayTra_ThucTe", SqlDbType.SmallDateTime, 3, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_daNgayTra_ThucTe));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@TonTai", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_bTonTai));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ID_MuonSach", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, m_iID_MuonSach));

                if (m_bMainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    m_scoMainConnection.Open();
                }
                else
                {
                    if (m_cpMainConnectionProvider.IsTransactionPending)
                    {
                        scmCmdToExecute.Transaction = m_cpMainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                m_iID_MuonSach = (SqlInt32)scmCmdToExecute.Parameters["@ID_MuonSach"].Value;
                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("clsMuonTraSach::Insert::Error occured.", ex);
            }
            finally
            {
                if (m_bMainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    m_scoMainConnection.Close();
                }
                scmCmdToExecute.Dispose();
            }
        }


        public override bool Update()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_MuonTraSach_Update]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = m_scoMainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ID_MuonSach", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iID_MuonSach));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ID_DocGia", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iID_DocGia));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@NgayMuon", SqlDbType.SmallDateTime, 3, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_daNgayMuon));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@NgayTra", SqlDbType.SmallDateTime, 3, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_daNgayTra));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@NgayTra_ThucTe", SqlDbType.SmallDateTime, 3, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_daNgayTra_ThucTe));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@TonTai", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_bTonTai));

                if (m_bMainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    m_scoMainConnection.Open();
                }
                else
                {
                    if (m_cpMainConnectionProvider.IsTransactionPending)
                    {
                        scmCmdToExecute.Transaction = m_cpMainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("clsMuonTraSach::Update::Error occured.", ex);
            }
            finally
            {
                if (m_bMainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    m_scoMainConnection.Close();
                }
                scmCmdToExecute.Dispose();
            }
        }


        public override bool Delete()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_MuonTraSach_Delete]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = m_scoMainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ID_MuonSach", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iID_MuonSach));

                if (m_bMainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    m_scoMainConnection.Open();
                }
                else
                {
                    if (m_cpMainConnectionProvider.IsTransactionPending)
                    {
                        scmCmdToExecute.Transaction = m_cpMainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("clsMuonTraSach::Delete::Error occured.", ex);
            }
            finally
            {
                if (m_bMainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    m_scoMainConnection.Close();
                }
                scmCmdToExecute.Dispose();
            }
        }


        public override DataTable SelectOne()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_MuonTraSach_SelectOne]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("MuonTraSach");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = m_scoMainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ID_MuonSach", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iID_MuonSach));

                if (m_bMainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    m_scoMainConnection.Open();
                }
                else
                {
                    if (m_cpMainConnectionProvider.IsTransactionPending)
                    {
                        scmCmdToExecute.Transaction = m_cpMainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                if (dtToReturn.Rows.Count > 0)
                {
                    m_iID_MuonSach = (Int32)dtToReturn.Rows[0]["ID_MuonSach"];
                    m_iID_DocGia = dtToReturn.Rows[0]["ID_DocGia"] == System.DBNull.Value ? SqlInt32.Null : (Int32)dtToReturn.Rows[0]["ID_DocGia"];
                    m_daNgayMuon = dtToReturn.Rows[0]["NgayMuon"] == System.DBNull.Value ? SqlDateTime.Null : (DateTime)dtToReturn.Rows[0]["NgayMuon"];
                    m_daNgayTra = dtToReturn.Rows[0]["NgayTra"] == System.DBNull.Value ? SqlDateTime.Null : (DateTime)dtToReturn.Rows[0]["NgayTra"];
                    m_daNgayTra_ThucTe = dtToReturn.Rows[0]["NgayTra_ThucTe"] == System.DBNull.Value ? SqlDateTime.Null : (DateTime)dtToReturn.Rows[0]["NgayTra_ThucTe"];
                    m_bTonTai = (bool)dtToReturn.Rows[0]["TonTai"];
                }
                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("clsMuonTraSach::SelectOne::Error occured.", ex);
            }
            finally
            {
                if (m_bMainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    m_scoMainConnection.Close();
                }
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }


        public override DataTable SelectAll()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_MuonTraSach_SelectAll]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("MuonTraSach");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = m_scoMainConnection;

            try
            {

                if (m_bMainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    m_scoMainConnection.Open();
                }
                else
                {
                    if (m_cpMainConnectionProvider.IsTransactionPending)
                    {
                        scmCmdToExecute.Transaction = m_cpMainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("clsMuonTraSach::SelectAll::Error occured.", ex);
            }
            finally
            {
                if (m_bMainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    m_scoMainConnection.Close();
                }
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }


        #region Class Property Declarations
        public SqlInt32 ID_MuonSach
        {
            get
            {
                return m_iID_MuonSach;
            }
            set
            {
                SqlInt32 iID_MuonSachTmp = (SqlInt32)value;
                if (iID_MuonSachTmp.IsNull)
                {
                    throw new ArgumentOutOfRangeException("iD_MuonSach", "iD_MuonSach can't be NULL");
                }
                m_iID_MuonSach = value;
            }
        }


        public SqlInt32 ID_DocGia
        {
            get
            {
                return m_iID_DocGia;
            }
            set
            {
                m_iID_DocGia = value;
            }
        }


        public SqlDateTime NgayMuon
        {
            get
            {
                return m_daNgayMuon;
            }
            set
            {
                m_daNgayMuon = value;
            }
        }


        public SqlDateTime NgayTra
        {
            get
            {
                return m_daNgayTra;
            }
            set
            {
                m_daNgayTra = value;
            }
        }


        public SqlDateTime NgayTra_ThucTe
        {
            get
            {
                return m_daNgayTra_ThucTe;
            }
            set
            {
                m_daNgayTra_ThucTe = value;
            }
        }


        public SqlBoolean TonTai
        {
            get
            {
                return m_bTonTai;
            }
            set
            {
                m_bTonTai = value;
            }
        }
        #endregion
    }
}
