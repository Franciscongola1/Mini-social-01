using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Collections.Concurrent;

namespace HastagMusic.Models
{
    public static class AcessoBancoDados
    {
        private static string strcon = @"d:\Banco.db";
        public static void AddUserToBD(Users user)
        {
            using SQLiteConnection conexao = new();
            try{
                conexao.ConnectionString = "Data Source="+strcon;
                conexao.Open(); //# abrindo a conexão...

                //*Instrução SQLite
                string sqlcomando = "Insert into tb_users(id,firstname,lastname,username,email,senha,estilo,modo)Values('"+user.Id+"','"+user.FirstName+"','"+user.LastName+"','"+user.UserName+"','"+user.Email+"','"+user.Senha+"','"+user.Estilo+"','"+user.Modo+"')";
                SQLiteCommand cmd =new(sqlcomando,conexao);

                //*exeutando a instrução.............
                cmd.ExecuteNonQuery();

                conexao.Close();//*fechando a conexão
            }catch(SQLiteException erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public static  void AddPostToBD(PostViewModel post)
        {
            using SQLiteConnection conexao = new();
            try{
                conexao.ConnectionString = "Data Source="+strcon;
                conexao.Open(); //# abrindo a conexão...

                //*Instrução SQLite
                string sqlcomando = "Insert into tb_post(id,userID,content,username,file,data,tipo)Values('"+post.Id+"','"+post.UserID+"','"+post.Content+"','"+post.UserName+"','"+post.FileName+"','"+post.Hora+"','"+post.Tipo+"')";
                SQLiteCommand cmd =new(sqlcomando,conexao);

                //*exeutando a instrução.............
                cmd.ExecuteNonQueryAsync();

                conexao.Close();//*fechando a conexão
            }catch(SQLiteException erro)
            {
                throw new Exception(erro.Message);
            }
        }


        public static ConcurrentBag<Users>GetUsersToBD()
        {
            ConcurrentBag<Users>SetUsersToList = new();
            using SQLiteConnection conexao = new();
            try
            {
                conexao.ConnectionString = "Data Source="+strcon;
                conexao.Open();

                //*Sql instrução 
                string sqlcmd = "Select id,firstname,lastname,username,email,senha,estilo,modo from tb_users";
                SQLiteCommand cmd = new(sqlcmd,conexao);
                SQLiteDataReader read = cmd.ExecuteReader();
                while(read.Read())
                {
                    //# Inserido os dados na lista
                    SetUsersToList.Add(new Users{
                        Id= read.GetValue(0).ToString(),
                        FirstName = read.GetValue(1).ToString(),
                        LastName = read.GetValue(2).ToString(),
                        UserName = read.GetValue(3).ToString(),
                        Email = read.GetValue(4).ToString(),
                        Senha = read.GetValue(5).ToString(),
                        Estilo = read.GetValue(6).ToString(),
                        Modo = read.GetValue(7).ToString()
                    });
                }
                return SetUsersToList;
            }
            catch(SQLiteException erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public static ConcurrentBag<PostViewModel>GetPostToBD()
        {
            ConcurrentBag<PostViewModel>SetPostToList = new();
            using SQLiteConnection conexao = new();
            try
            {
                conexao.ConnectionString = "Data Source="+strcon;
                conexao.Open();

                //*Sql instrução 
                string sqlcmd = "Select id,userId,content,username,file,data,tipo from tb_post";
                SQLiteCommand cmd = new(sqlcmd,conexao);
                SQLiteDataReader read = cmd.ExecuteReader();
                while(read.Read())
                {
                    //# Inserido os dados na lista
                    SetPostToList.Add(new PostViewModel{
                        Id= read.GetValue(0).ToString(),
                        UserID = read.GetValue(1).ToString(),
                        Content = read.GetValue(2).ToString(),
                        UserName = read.GetValue(3).ToString(),
                        FileName = read.GetValue(4).ToString(),
                        Hora = Convert.ToDateTime(read.GetValue(5)),
                        Type = read.GetValue(6).ToString()
                    });
                }
                return SetPostToList;
            }
            catch(SQLiteException erro)
            {
                throw new Exception(erro.Message);
            }
        }

        //---------------------------------------------------------
        public static List<Seguir> ConsultaSeguidorToBD()
        {
            List<Seguir> list = new();
           using SQLiteConnection con =new();
                con.ConnectionString = "Data source="+strcon;
                try
                {
                    con.Open();
                    string sqlcmd = "Select id,seguidoID,seguidorID From tb_seguir";
                    SQLiteCommand cmd =new(sqlcmd,con);
                    SQLiteDataReader rd = cmd.ExecuteReader();
                    while(rd.Read())
                    {
                        list.Add(new Seguir{
                            ID =Convert.ToInt32(rd.GetValue(0)),
                            SeguidoID = rd.GetValue(1).ToString(),
                            SeguidorID = rd.GetValue(2).ToString()
                        });
                    }
                    con.Close();
                }
                catch(SQLiteException erro)
                {
                    throw new Exception(erro.Message);
                }
           return list;
        }
        //----------------------------------------------------------------------------

        public static void AddSeguidorToBD(Seguir seguir)
        {
            using SQLiteConnection con = new();
            con.ConnectionString = "Data source=" + strcon;
            try
            {
                con.Open();
                string sqlcmd = "Insert into tb_seguir(seguidoID,seguidorID)Values('" + seguir.SeguidoID+ "','" + seguir.SeguidorID + "')";
                SQLiteCommand cmd = new(sqlcmd, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SQLiteException erro)
            {
                throw new Exception(erro.Message);
            }
        }

        // ------------------------------------------------------

        // METODO RESPONSAVEL PELA FUNÇÃO DEIXAR DE SEGUIR
        public static async void DeletarSeguidorToBD(int seguidoID)
        {
            await Task.Run(() =>
            {
                Task.Delay(100).Wait();
            });
            using SQLiteConnection con = new();
            con.ConnectionString = "Data source=" + strcon;
            try
            {
                con.Open();
                string sqlcmd = "DELETE from tb_seguir WHERE id=" +seguidoID;
                SQLiteCommand cmd = new(sqlcmd, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SQLiteException erro)
            {
                throw new Exception(erro.Message);
            }
        }

        /*----------------------------------------------------------------*/
        //Metodo adicionar gostos
        public  static async void AddGostosToBD(Gostar gosto)
        {
            await Task.Run(() =>
            {
                Task.Delay(100).Wait();
            });
             using SQLiteConnection con = new();
            con.ConnectionString = "Data source=" + strcon;
            try
            {
                con.Open();
                string sqlcmd = "Insert into tb_gostar(userid,postid)Values('"+gosto.UserID+"','"+gosto.PostID+"')";
                SQLiteCommand cmd = new(sqlcmd, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SQLiteException erro)
            {
                throw new Exception("Ocorreu um erro ao reagir "+erro.Message);
            }
        }

        public static ConcurrentBag<Gostar> ConsultaGostosToBD()
        {
            
            ConcurrentBag<Gostar> list = new();
           using SQLiteConnection con =new();
                con.ConnectionString = "Data source="+strcon;
                try
                {
                    con.Open();
                    string sqlcmd = "Select id,userid,postid From tb_gostar";
                    SQLiteCommand cmd =new(sqlcmd,con);
                    SQLiteDataReader rd = cmd.ExecuteReader();
                    while(rd.Read())
                    {
                        list.Add(new Gostar{
                            ID =Convert.ToInt32(rd.GetValue(0)),
                            UserID = rd.GetValue(1).ToString(),
                            PostID = rd.GetValue(2).ToString()
                        });
                    }
                    con.Close();
                }
                catch(SQLiteException erro)
                {
                    throw new Exception(erro.Message);
                }
           return list;
        }

        public static void DeletarGostoToBD(int gostoID)
        {
            using SQLiteConnection con = new();
            con.ConnectionString = "Data source=" + strcon;
            try
            {
                con.Open();
                string sqlcmd = "DELETE from tb_gostar WHERE id=" +gostoID;
                SQLiteCommand cmd = new(sqlcmd, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SQLiteException erro)
            {
                throw new Exception(erro.Message);
            }
        }

          //---------------------------------------------------------
        //Metodo adicionar comentarios
        public static void AddComentariosToBD(Comments comment)
        {
             using SQLiteConnection con = new();
            con.ConnectionString = "Data source=" + strcon;
            try
            {
                con.Open();
                string sqlcmd = "Insert into tb_comentarios(userid,postid,comentario,data,username)Values('"+comment.UserID+"','"+comment.PostID+"','"+comment.Comentarios+"','"+comment.Hora+"','"+comment.Nomeuser+"')";
                SQLiteCommand cmd = new(sqlcmd, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SQLiteException erro)
            {
                throw new Exception("Ocorreu um erro ao comentar "+erro.Message);
            }
        }
        public static ConcurrentBag<Comments>ConsultarComments()
        {
            ConcurrentBag<Comments> listCom = new();
           using SQLiteConnection con =new();
                con.ConnectionString = "Data source="+strcon;
                try
                {
                    con.Open();
                    string sqlcmd = "Select id,userID,postID,comentario,data,username From tb_comentarios";
                    SQLiteCommand cmd =new(sqlcmd,con);
                    SQLiteDataReader rd = cmd.ExecuteReader();
                    while(rd.Read())
                    {
                        listCom.Add(new Comments
                        {
                            ID = Convert.ToInt32(rd.GetValue(0)),
                            UserID = rd.GetValue(1).ToString(),
                            PostID = rd.GetValue(2).ToString(),
                            Comentarios = rd.GetValue(3).ToString(),
                            Hora = rd.GetValue(4).ToString(),
                            Nomeuser = rd.GetValue(5).ToString()
                        });
                    }
                    con.Close();
                }
                catch(SQLiteException erro)
                {
                    throw new Exception(erro.Message);
                }
           return listCom;
        }
    }
}