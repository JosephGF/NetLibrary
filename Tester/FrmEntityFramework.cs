using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetLibrary.DataBase.SQL;
namespace Tester
{
    public partial class FrmDataBase : Form
    {
        [SqlAttributes.Table("USERS")]
        public class Person
        {
            public int Id_User { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public short Rol { get; set; }
            public string Nombre { get; set; }
            public DateTime Fecha_Modificacion { get; set; }
            public bool Activo { get; set; }
            public string Nif { get; set; }
            public int Id_TipoTrabajo { get; set; }
            public bool EnvioMIR { get; set; }
            [SqlAttributes.Ignore]
            public List<Direction> Directions { get; set; }
            [SqlAttributes.Ignore]
            public Direction Current { get; set; }
        }

        public class Direction
        {
            [SqlAttributes.Column(Name = "ID_DIR")]
            public int ID { get; set; }
            public string Name { get; set; }
            public int Number { get; set; }
            public int IdCity { get; set; }
            public City City { get; set; }
        }

        public class City
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public FrmDataBase()
        {
            InitializeComponent();
        }

        private void btnGenerateSQL_Click(object sender, EventArgs e)
        {
            //var a = new SqlBuilderLambda<Person>().Count<int>();
            ///var a = new SqlBuilderLambda<Person>().Update(new { Activo = true }).Where(p => p.Id_User == 1020).Commit();
            //
            //var a = new { Name = "ANY", Anyo = 8 };
            /*this.txSql.Text = new QueryBuilder<Person>(m => m..StartsWith("A") && m.Age == a.Anyo && (m.SurName == a.Name || m.BirthDay > DateTime.MinValue))
                                                   .OrderBy(m => m.Name)
                                                   .OrderThenByDescending(m => m.Age)
                                                   .GroupBy(m => m.Name).SQL;*/
            //this.txSql.Text = new QueryBuilder<Person>(m => m.Current.Name.Equals("SUN") && m.Age > 8).SQL;
            /*this.txSql.Text = new SQLBuilderLambda<Person>().Where<Direction>((p, d) => p.IdDirection != d.ID)
                                                           .Or(P => P.IdDirection.Equals(99) && P.SurName.EndsWith("O"))
                                                           .GroupBy(p => p.Name)
                                                           .GroupThenBy(p => p.SurName)
                                                           .Having(p => p.Sum(pS=> pS.Age).Equals(5)).SQL;*/

           // this.txSql.Text = new SqlBuilderLambda<Person>().Where(p => p.ID == 5).InnerJoin<Direction>((p, d) => d.ID == p.IdDirection && d.Number.Equals(18)).Max<short>(p => p.Age).ToString();
            this.txSql.Text = new SqlBuilderLambda<Person>().Where(p => p.Nif == null).ToString();
        }
    }
}
