using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session6
{
    public partial class AMONICAirlines : Form
    {

        SQLConnect r = new SQLConnect();
        String sql;
        int second = 1;
        public AMONICAirlines()
        {
            InitializeComponent();
        }

        private void AMONICAirlines_Load(object sender, EventArgs e)
        {

            //confirmed flight
            sql = @"Select Count(*) from Tickets 
            inner join Schedules on Tickets.ScheduleID=Schedules.ID
            where Tickets.Confirmed=1 and Schedules.Date>= DATEADD(MONTH,-30,GETDATE())";
            r.DisplaySingle(sql);
            lblconfirmed.Text = r.getf1();


            //cancel flight
            sql = @"Select Count(*) from Tickets 
            inner join Schedules on Tickets.ScheduleID=Schedules.ID
            where Tickets.Confirmed=0 and Schedules.Date>= DATEADD(MONTH,-30,GETDATE())";
            r.DisplaySingle(sql);
            lblcancell.Text = r.getf1();

            //minutes flight
            sql = @"Select cast(cast(avg(cast(cast(Time as datetime)as float))as datetime)as time)  from Tickets 
            inner join Schedules on Tickets.ScheduleID=Schedules.ID
            where Tickets.Confirmed=1 and Schedules.Date>= DATEADD(MONTH,-30,GETDATE())";
            r.DisplaySingle(sql);
            lblave.Text = r.getf1();

            //top customers
            sql = @"Select TOP 3  Firstname+' '+Lastname as [name],Count(*)
            from Tickets
            inner join Schedules on Tickets.ScheduleID=Schedules.ID where
            Tickets.ID>1 and 
            Tickets.Confirmed=1 and Schedules.Date>= DATEADD(MONTH,-30,GETDATE())
            group by Firstname ,Lastname order by Count(*) desc";
            dataGridView1.DataSource = r.MultipleData(sql).Tables["tbl"];
            lblname1.Text = dataGridView1.Rows[0].Cells[0].Value.ToString() + "( " + dataGridView1.Rows[0].Cells[1].Value.ToString() + ")";
            lblname2.Text = dataGridView1.Rows[1].Cells[0].Value.ToString() + "( " + dataGridView1.Rows[1].Cells[1].Value.ToString() + ")";
            lblname3.Text = dataGridView1.Rows[2].Cells[0].Value.ToString() + "( " + dataGridView1.Rows[2].Cells[1].Value.ToString() + ")";

            //top revenue
            sql = @"SELECT Top 3 Airports.Name,Count(*) 
            from Tickets
            inner join Schedules on Tickets.ScheduleID=Schedules.ID 
            inner join Routes on Schedules.RouteID=Routes.ID
            inner join Airports on Routes.DepartureAirportID=Airports.ID
            where
            Tickets.ID>1 and 
            Tickets.Confirmed=1 and Schedules.Date>= DATEADD(MONTH,-30,GETDATE())
            group by Airports.Name  order by Count(*) desc";
            dataGridView2.DataSource = r.MultipleData(sql).Tables["tbl"];
            lblof1.Text = dataGridView2.Rows[0].Cells[0].Value.ToString() + "( " + dataGridView2.Rows[0].Cells[1].Value.ToString() + ")";
            lblof2.Text = dataGridView2.Rows[1].Cells[0].Value.ToString() + "( " + dataGridView2.Rows[1].Cells[1].Value.ToString() + ")";
            lblof3.Text = dataGridView2.Rows[2].Cells[0].Value.ToString() + "( " + dataGridView2.Rows[2].Cells[1].Value.ToString() + ")";

            //busies flight
            sql = @"Select Top 1 Schedules.Date,Count(*)
            from Tickets 
            inner join Schedules on Tickets.ScheduleID=Schedules.ID
            where Tickets.Confirmed=1 and Schedules.Date>= DATEADD(MONTH,-30,GETDATE())
            group by Schedules.Date order by Count(*) desc";
            r.DisplaySingle(sql);
            date1.Text = Convert.ToDateTime(r.getf1()).ToShortDateString();
            b1.Text = r.getf2();

            //busies flight
            sql = @"Select Top 1 Schedules.Date,Count(*)
            from Tickets 
            inner join Schedules on Tickets.ScheduleID=Schedules.ID
            where Tickets.Confirmed=1 and Schedules.Date>= DATEADD(MONTH,-30,GETDATE())
            group by Schedules.Date order by Count(*) asc";
            r.DisplaySingle(sql);
            date2.Text = Convert.ToDateTime(r.getf1()).ToShortDateString();
            b2.Text = r.getf2();

            //yesterday
            sql = @"Select SUM(
	        case when Tickets.CabinTypeID=1 then cast(Schedules.EconomyPrice as smallmoney)
		     when Tickets.CabinTypeID=5 then cast((Schedules.EconomyPrice *.35+Schedules.EconomyPrice)as smallmoney)
		     when Tickets.CabinTypeID=3 then cast((Schedules.EconomyPrice *.35+Schedules.EconomyPrice)*.30+
		     (Schedules.EconomyPrice *.35+Schedules.EconomyPrice)as smallmoney) end)
            from Tickets
            inner join Schedules on Tickets.ScheduleID=Schedules.ID
            where Tickets.Confirmed=1 and Schedules.Date>= DATEADD(DAY,-1,'2017-10-28')";
            r.DisplaySingle(sql);
            
            lblyest.Text = r.getf1();

            //LAST TWO DAYS 
            sql = @"Select SUM(
	        case when Tickets.CabinTypeID=1 then cast(Schedules.EconomyPrice as smallmoney)
		     when Tickets.CabinTypeID=5 then cast((Schedules.EconomyPrice *.35+Schedules.EconomyPrice)as smallmoney)
		     when Tickets.CabinTypeID=3 then cast((Schedules.EconomyPrice *.35+Schedules.EconomyPrice)*.30+
		     (Schedules.EconomyPrice *.35+Schedules.EconomyPrice)as smallmoney) end)
            from Tickets
            inner join Schedules on Tickets.ScheduleID=Schedules.ID
            where Tickets.Confirmed=1 and Schedules.Date>= DATEADD(DAY,-2,'2017-10-28')";
            r.DisplaySingle(sql);

            lasttwo.Text = r.getf1();

            //LAST THREE DAYS 
            sql = @"Select SUM(
	        case when Tickets.CabinTypeID=1 then cast(Schedules.EconomyPrice as smallmoney)
		     when Tickets.CabinTypeID=5 then cast((Schedules.EconomyPrice *.35+Schedules.EconomyPrice)as smallmoney)
		     when Tickets.CabinTypeID=3 then cast((Schedules.EconomyPrice *.35+Schedules.EconomyPrice)*.30+
		     (Schedules.EconomyPrice *.35+Schedules.EconomyPrice)as smallmoney) end)
            from Tickets
            inner join Schedules on Tickets.ScheduleID=Schedules.ID
            where Tickets.Confirmed=1 and Schedules.Date>= DATEADD(DAY,-3,'2017-10-28')";
            r.DisplaySingle(sql);

            lastthree.Text = r.getf1();

            //last week
            sql = @"Select Count(Schedules.Time) as t,Count(Schedules.Time)-176 as a
             from Schedules 
             inner join Tickets on Tickets.ScheduleID=Schedules.ID 
             where AircraftID=1 and Tickets.Confirmed=1 and Schedules.Date>= DATEADD(WEEK,-1,'2017-10-28')";
            r.DisplaySingle(sql);
            air1.Text = r.getf2();

            sql = @"Select Count(Schedules.Time) as t,Count(Schedules.Time)-169 as a
             from Schedules 
             inner join Tickets on Tickets.ScheduleID=Schedules.ID 
             where AircraftID=2 and Tickets.Confirmed=1 and Schedules.Date>= DATEADD(WEEK,-1,'2017-10-28')";
            r.DisplaySingle(sql);
            air2.Text = r.getf2();
            int last = 0;
            last = (Convert.ToInt16(air1.Text)  +Convert.ToInt16(air2.Text))/100 ;
            thiss.Text = last.ToString();

            //last 2 week
            sql = @"Select Count(Schedules.Time) as t,Count(Schedules.Time)-176 as a
             from Schedules 
             inner join Tickets on Tickets.ScheduleID=Schedules.ID 
             where AircraftID=1 and Tickets.Confirmed=1 and Schedules.Date>= DATEADD(WEEK,-2,'2017-10-28')";
            r.DisplaySingle(sql);
            label27.Text = r.getf2();

            sql = @"Select Count(Schedules.Time) as t,Count(Schedules.Time)-169 as a
             from Schedules 
             inner join Tickets on Tickets.ScheduleID=Schedules.ID 
             where AircraftID=2 and Tickets.Confirmed=1 and Schedules.Date>= DATEADD(WEEK,-2,'2017-10-28')";
            r.DisplaySingle(sql);
            label22.Text = r.getf2();
            
            last = (Convert.ToInt16(label27.Text) + Convert.ToInt16(label22.Text)) / 100;
            las.Text = last.ToString();
            //last 3 week
            sql = @"Select Count(Schedules.Time) as t,Count(Schedules.Time)-176 as a
             from Schedules 
             inner join Tickets on Tickets.ScheduleID=Schedules.ID 
             where AircraftID=1 and Tickets.Confirmed=1 and Schedules.Date>= DATEADD(WEEK,-3,'2017-10-28')";
            r.DisplaySingle(sql);
            l1.Text = r.getf2();

            sql = @"Select Count(Schedules.Time) as t,Count(Schedules.Time)-169 as a
             from Schedules 
             inner join Tickets on Tickets.ScheduleID=Schedules.ID 
             where AircraftID=2 and Tickets.Confirmed=1 and Schedules.Date>= DATEADD(WEEK,-3,'2017-10-28')";
            r.DisplaySingle(sql);
            l.Text = r.getf2();

            last = (Convert.ToInt16(l.Text) + Convert.ToInt16(l1.Text)) / 100;
            tw.Text = last.ToString();

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Start();
            second = second + 1;
            lblsec.Text = second.ToString();


        }

        private void date1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
