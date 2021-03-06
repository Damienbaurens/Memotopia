﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GroupeDNavalBattle
{
    public class SeaElement
    {
        //Attributs
        public Button button { get; set; } //Le { get; set; } permet d'avoir directement le getter et le setter par défaut
        public int posX { get; }
        public int posY { get;}
        public int player { get; }

        Boolean _known;
        public Boolean known
        {
            get => _known;
            set
            {
                this._known = value;
                if(this._known == true)
                {
                    this.state = this.state;
                }
                else
                {
                    this.button.Background = BrushSet.waterBrush;
                }
            }
        } 
             
        Boolean _clickable;
        public Boolean clickable 
        {
            get => _clickable;
           
            set 
            {
                this._clickable = value;
                if(this._clickable == true)
                {
                    this.button.IsEnabled = true;
                    this.state = this.state ;
                }
                else
                {
                    this.button.IsEnabled = false;
                    switch (this.state)
                    {
                        case State.Water:
                            this.button.Background = BrushSet.unclickableBrush;
                            break;
                        default:
                            break;

                    }
                }
            } 
        }
        String name;

        State _state;
        public State state
        {
            get => _state;
            set
            {
                this._state = value;
                if(this.known == true)
                {
                    switch (_state)
                    {
                        case State.Water:
                            this.button.Background = BrushSet.waterBrush;
                            break;
                        case State.Boat:
                            this.button.Background = BrushSet.boatBrush;
                            break;
                        case State.Touched:
                            this.button.Background = BrushSet.touchedBrush;
                            break;
                        case State.Sunk:
                            this.button.Background = BrushSet.sunkBrush;
                            break;
                        case State.Plouf:
                            this.button.Background = BrushSet.ploufBrush;
                            break;

                    }
                }
                
            }
        }


        //Constructeur
        public SeaElement(int posX, int posY, int player, Boolean known)
        {
            
            this.posX = posX; //position en abscisse (valeurs de 1 à 10, le placement du bouton se fera en multipliant)
            this.posY = posY; //position en ordonnée
            
            this.player = player; //numéro du joueur
            this.button = new Button();
            this.known = known;
            
            if (this.known == false)
            {
                
                this.button.Background = BrushSet.waterBrush;
            }
            else
            {
                this.state = this.state;
            }

            this.name = "P" + player.ToString() + posX.ToString() + posY.ToString();

            this.button.Name = this.name + "Button";
            this.button.Content = ""; //bouton vide

            // largeur et hauteur du bouton, on les fait carrés pour si on veut mettre des images dessus un jour
            this.button.Width = 50;
            this.button.Height = 50;

            this.button.Background = BrushSet.waterBrush; //couleur de fond du bouton, par défaut c'est de l'eau
            this.button.BorderThickness = new Thickness(1, 1, 1, 1); //épaisseur en pixel de la bordure du bouton
            this.button.BorderBrush = BrushSet.borderBrush; //couleur de la bordure

            // point d'ancrage du bouton : pour le placer, on compte à partir du coin en haut à gauche
            this.button.HorizontalAlignment = HorizontalAlignment.Left;
            this.button.VerticalAlignment = VerticalAlignment.Top;

            //position exacte sur le board
            this.button.Margin = new Thickness(100 + (player - 1) * 580 + (posX - 1) * 50, 135 + (posY - 1) * 50, 0, 0);

            //event quand on presse le bouton
            this.button.Click += Button_Click;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonBuffer.setPressedSeaElement(this);
            GameManager.OnClick(this);
        }
        
    }
}
