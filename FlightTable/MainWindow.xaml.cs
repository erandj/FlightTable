using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FlightTable
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] bad = new string[3]
        {
            "Сел",
            "Перенесен",
            "Посадка задержана",
        };
        public MainWindow()
        {
            InitializeComponent();
            SetDataGrid();

            DispatcherTimer UpdateDataGridTimer = new DispatcherTimer();
            UpdateDataGridTimer.Interval = TimeSpan.FromSeconds(10);
            UpdateDataGridTimer.Tick += UpdateDataGrid;
            UpdateDataGridTimer.Start();
        }
        private void UpdateDataGrid(object sender, EventArgs e)
        {
            SetDataGrid();
        }

        private void SetDataGrid()
        {
            List<FlightTableElement> elements = new List<FlightTableElement>();

            using (FlightEntities context = new FlightEntities())
            {
                IQueryable<ScheduledRoute> scheduledRoutes = context.ScheduledRoute.Where(x => !bad.Contains(x.ScheduledRouteStatus.name));

                foreach (var scheduledRoute in scheduledRoutes)
                {
                    var route = scheduledRoute.Route;

                    string terminals = "";
                    string gates = "";

                    foreach (var terminal in scheduledRoute.Terminal)
                        terminals += terminal.number + ", ";

                    foreach (var gate in scheduledRoute.Gate)
                        gates += gate.number + ", ";

                    FlightTableElement element = new FlightTableElement
                    {
                        Id = scheduledRoute.id,
                        Airline = route.Airline.name,
                        Time = scheduledRoute.actual_departure_datetime != null ?
                                                    scheduledRoute.actual_departure_datetime.ToString() 
                                                    : scheduledRoute.scheduled_departure_datetime.ToString(),
                        Flight = FlightInfo.GetFlightCode(route, context),
                        Aircraft = scheduledRoute.Aircraft.name,
                        Destination = route.Airport1.name,
                        Status = scheduledRoute.ScheduledRouteStatus.name,
                        Terminal = terminals != "" ? terminals.Remove(terminals.Count() - 2) : "",
                        Gate = gates != "" ? gates.Remove(gates.Count() - 2) : "",
                    };

                    elements.Add(element);
                }
            }

            elements.Sort((x, y) => DateTime.Compare(DateTime.Parse(y.Time), DateTime.Parse(x.Time)));

            FlightTable.ItemsSource = null;
            FlightTable.ItemsSource = elements;
        }
    }
}
