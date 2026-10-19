using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Evaluation_Sommative_UA1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


            private void ValiderFormulaire(object sender, EventArgs e)
            {
                // Vérifier que tous les composants sont bien initialisés
                if (CbxEntreprise == null || CbxProgramme == null || CbxStage == null ||
                    TxtPoste == null || TxtLieu == null || DtpDebut == null ||
                    DtpFin == null || TxtDescription == null || TxtNombreDePoste == null ||
                    CbxStatut == null || BtnPublier == null || LblError == null)
                {
                    return;
                }

                // 1. Vérification des champs obligatoires remplis
                bool entrepriseRemplie = CbxEntreprise.SelectedIndex > 0;
                bool programmeRempli = CbxProgramme.SelectedIndex > 0;
                bool stageRempli = CbxStage.SelectedIndex > 0;
                bool statutRempli = CbxStatut.SelectedIndex > 0;

                bool posteRempli = !string.IsNullOrWhiteSpace(TxtPoste.Text);
                bool lieuRempli = !string.IsNullOrWhiteSpace(TxtLieu.Text);
                bool descriptionRemplie = !string.IsNullOrWhiteSpace(TxtDescription.Text) && TxtDescription.Text != "Décrivez les tâches, les responsabilités...";
                bool nombrePosteRempli = !string.IsNullOrWhiteSpace(TxtNombreDePoste.Text);

                bool datesRemplies = DtpDebut.SelectedDate.HasValue && DtpFin.SelectedDate.HasValue;

                bool tousChampsRemplis = entrepriseRemplie && programmeRempli && stageRempli &&
                                         posteRempli && lieuRempli && descriptionRemplie &&
                                         nombrePosteRempli && statutRempli && datesRemplies;

                // 2. Vérification de la cohérence des dates
                bool datesInvalides = false;
                if (datesRemplies)
                {
                    // Si Date Début > Date Fin
                    if (DtpDebut.SelectedDate.Value > DtpFin.SelectedDate.Value)
                    {
                        datesInvalides = true;
                    }
                }

                // 3. Application des règles visuelles et d'activation
                if (datesInvalides)
                {
                    LblError.Visibility = Visibility.Visible;
                    BtnPublier.IsEnabled = false; // Empêche la publication
                }
                else
                {
                    LblError.Visibility = Visibility.Collapsed;
                    // Le bouton s'active uniquement si tout est rempli ET que les dates sont valides
                    BtnPublier.IsEnabled = tousChampsRemplis;
                }
            }

            private void BtnPublier_Click(object sender, RoutedEventArgs e)
            {
                MessageBox.Show("L'offre de stage a été publiée avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }