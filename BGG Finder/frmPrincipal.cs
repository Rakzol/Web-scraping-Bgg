using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace BGG_Finder
{
    public partial class frmPrincipal : Form
    {
        String directorioBase;
        String directorioXmlUsuarios;
        String directorioXmlJuegos;
        String directorioXmlColecciones;
        String directorioXmlJugadas;
        String directorioImagenesJuegos;
        String directorioImagenesAutores;
        Repertorio repertorio;
        public frmPrincipal()
        {
            InitializeComponent();
        }
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            directorioBase = Application.LocalUserAppDataPath;

            directorioXmlUsuarios = directorioBase + "/Usuarios";
            if (!Directory.Exists(directorioXmlUsuarios))
            {
                Directory.CreateDirectory(directorioXmlUsuarios);
            }

            directorioXmlJuegos = directorioBase + "/Juegos";
            if (!Directory.Exists(directorioXmlJuegos))
            {
                Directory.CreateDirectory(directorioXmlJuegos);
            }

            directorioXmlColecciones = directorioBase + "/Colecciones";
            if (!Directory.Exists(directorioXmlColecciones))
            {
                Directory.CreateDirectory(directorioXmlColecciones);
            }

            directorioXmlJugadas = directorioBase + "/Jugadas";
            if (!Directory.Exists(directorioXmlJugadas))
            {
                Directory.CreateDirectory(directorioXmlJugadas);
            }

            directorioImagenesJuegos = directorioBase + "/ImagenesJuegos";
            if (!Directory.Exists(directorioImagenesJuegos))
            {
                Directory.CreateDirectory(directorioImagenesJuegos);
            }

            directorioImagenesAutores = directorioBase + "/ImagenesAutores";
            if (!Directory.Exists(directorioImagenesAutores))
            {
                Directory.CreateDirectory(directorioImagenesAutores);
            }
        }
        private void btnBuscarUsuario_Click(object sender, System.EventArgs e)
        {
            try
            {
                progresoCarga.Visible = true;
                XmlDocument xmlRepertorio = Utilerias.consultarXmlRepertorio(txtNombreUsuario.Text, directorioXmlColecciones, directorioXmlJuegos, directorioImagenesJuegos, directorioImagenesAutores, directorioXmlJugadas, chkActualizarColeccion.Checked, chkActualizarJugadas.Checked, progresoCarga);
                repertorio = new Repertorio(xmlRepertorio);
                desplegarRepertorio();
                nodosItems(trvUsuario.Nodes);
                progresoCarga.Visible = false;
                progresoCarga.Value = 0;
            }
            catch (Exception ex)
            {
                progresoCarga.Visible = false;
                MessageBox.Show(ex.Message);
            }
        }
        void desplegarRepertorio()
        {
            limpiarListaImagenes(imlVistaArbol);

            /* Liberamos el espacio de los nodos */
            trvUsuario.Nodes.Clear();

            TreeView arbolPadre = new TreeView();
            arbolPadre.Nodes.Add("autores", "Autores", "autores", "autores");
            arbolPadre.Nodes["autores"].Tag = "autores";
            arbolPadre.Nodes.Add("juegos", "Juegos", "juegos", "juegos");
            arbolPadre.Nodes["juegos"].Tag = "juegos";
            arbolPadre.Nodes["juegos"].Nodes.Add("numerosJugadores", "# Jugadores", "numerosJugadores", "numerosJugadores");
            arbolPadre.Nodes["juegos"].Nodes["numerosJugadores"].Tag = "numerosJugadores";
            arbolPadre.Nodes["juegos"].Nodes.Add("categorias", "Categorías", "categorias", "categorias");
            arbolPadre.Nodes["juegos"].Nodes["categorias"].Tag = "categorias";
            arbolPadre.Nodes["juegos"].Nodes.Add("familias", "Familias", "familias", "familias");
            arbolPadre.Nodes["juegos"].Nodes["familias"].Tag = "familias";
            arbolPadre.Nodes["juegos"].Nodes.Add("mecanicas", "Mecánicas", "mecanicas", "mecanicas");
            arbolPadre.Nodes["juegos"].Nodes["mecanicas"].Tag = "mecanicas";
            arbolPadre.Nodes.Add("rivales", "Rivales", "rivales", "rivales");
            arbolPadre.Nodes["rivales"].Tag = "rivales";
            arbolPadre.Nodes["rivales"].Nodes.Add("nombresRivales", "Nombres", "nombresRivales", "nombresRivales");
            arbolPadre.Nodes["rivales"].Nodes["nombresRivales"].Tag = "nombresRivales";
            arbolPadre.Nodes["rivales"].Nodes.Add("juegosRivales", "Juegos", "juegosRivales", "juegosRivales");
            arbolPadre.Nodes["rivales"].Nodes["juegosRivales"].Tag = "juegosRivales";
            arbolPadre.Nodes.Add("resumen", "Resumen", "resumen", "resumen");
            arbolPadre.Nodes["resumen"].Tag = "resumen";

            /* Desplegamos los nodos de autores */
            foreach (Enlace autor in repertorio.autores.Values)
            {
                arbolPadre.Nodes["autores"].Nodes.Add(autor.id, autor.valor, "sinImagen", "sinImagen");
                arbolPadre.Nodes["autores"].Nodes[autor.id].Tag = "autor";
                if (File.Exists(directorioImagenesAutores + "/" + autor.id + ".jpg"))
                {
                    imlVistaArbol.Images.Add(directorioImagenesAutores + "/" + autor.id + ".jpg", Image.FromFile(directorioImagenesAutores + "/" + autor.id + ".jpg"));
                    arbolPadre.Nodes["autores"].Nodes[autor.id].ImageKey = directorioImagenesAutores + "/" + autor.id + ".jpg";
                    arbolPadre.Nodes["autores"].Nodes[autor.id].SelectedImageKey = directorioImagenesAutores + "/" + autor.id + ".jpg";
                }
            }

            /* Desplegamos los nodos de numeros juegadores */
            for (int numeroJugadores = repertorio.minimoJuegadores; numeroJugadores <= repertorio.maximoJugadores; numeroJugadores++)
            {
                arbolPadre.Nodes["juegos"].Nodes["numerosJugadores"].Nodes.Add(numeroJugadores.ToString(), numeroJugadores.ToString(), "numeroJugadores", "numeroJugadores");
                arbolPadre.Nodes["juegos"].Nodes["numerosJugadores"].Nodes[numeroJugadores.ToString()].Tag = "numeroJugadores";
            }

            /* Desplegamos los nodos de categorias */
            foreach (Enlace categoria in repertorio.categorias.Values)
            {
                arbolPadre.Nodes["juegos"].Nodes["categorias"].Nodes.Add(categoria.id, categoria.valor, "categoria", "categoria");
                arbolPadre.Nodes["juegos"].Nodes["categorias"].Nodes[categoria.id].Tag = "categoria";
            }

            /* Desplegamos los nodos de familias */
            foreach (Enlace familia in repertorio.familias.Values)
            {
                arbolPadre.Nodes["juegos"].Nodes["familias"].Nodes.Add(familia.id, familia.valor, "familia", "familia");
                arbolPadre.Nodes["juegos"].Nodes["familias"].Nodes[familia.id].Tag = "familia";
            }

            /* Desplegamos los nodos de mecanias */
            foreach (Enlace mecanica in repertorio.mecanicas.Values)
            {
                arbolPadre.Nodes["juegos"].Nodes["mecanicas"].Nodes.Add(mecanica.id, mecanica.valor, "mecanica", "mecanica");
                arbolPadre.Nodes["juegos"].Nodes["mecanicas"].Nodes[mecanica.id].Tag = "mecanica";
            }

            /* Desplegamos los nodos de nombres de rivales */
            foreach (Rival rival in repertorio.rivales.Values)
            {
                arbolPadre.Nodes["rivales"].Nodes["nombresRivales"].Nodes.Add(rival.nombre.ToLower(), rival.nombre, "rival", "rival");
                arbolPadre.Nodes["rivales"].Nodes["nombresRivales"].Nodes[rival.nombre.ToLower()].Tag = "rival";
            }

            /* Desplegamos y Clonamos los nodos de juegos en sus respectivos nodos padres */
            foreach (Juego juego in repertorio.juegos.Values)
            {
                TreeNode nodoJuego = new TreeNode(juego.nombre);
                nodoJuego.Name = juego.id;
                nodoJuego.Tag = "juego";
                if (File.Exists(directorioImagenesJuegos + "/" + juego.id + ".jpg"))
                {
                    imlVistaArbol.Images.Add(directorioImagenesJuegos + "/" + juego.id + ".jpg", Image.FromFile(directorioImagenesJuegos + "/" + juego.id + ".jpg"));
                    nodoJuego.ImageKey = directorioImagenesJuegos + "/" + juego.id + ".jpg";
                    nodoJuego.SelectedImageKey = directorioImagenesJuegos + "/" + juego.id + ".jpg";
                }
                else
                {
                    nodoJuego.ImageKey = "sinImagen";
                    nodoJuego.SelectedImageKey = "sinImagen";
                }

                /* Clonamos el nodo del juego en los nodos padres de autores a los que pertenezca */
                foreach (Enlace autor in juego.autores.Values)
                {
                    //Console.WriteLine("Desplegando juego en autor: " + autor.id);
                    arbolPadre.Nodes["autores"].Nodes[autor.id].Nodes.Add((TreeNode)nodoJuego.Clone());
                }

                /* Clonamos el nodo del juego en los nodos padres de numeros de jugadores a los que pertenezca */
                for (int numeroJugadores = juego.minimoJuegadores; numeroJugadores <= juego.maximoJugadores; numeroJugadores++)
                {
                    //Console.WriteLine("Desplegando juego en numero de juegadores: " + numeroJugadores);
                    arbolPadre.Nodes["juegos"].Nodes["numerosJugadores"].Nodes[numeroJugadores.ToString()].Nodes.Add((TreeNode)nodoJuego.Clone());
                }
                /*Cuando aun no se vota por el numero de jugadores es -1*/
                if (juego.recomendados != -1)
                {
                    if (!nodoJuego.ImageKey.Equals("sinImagen"))
                    {
                        Image imagen = Image.FromFile(nodoJuego.ImageKey);
                        Graphics.FromImage(imagen).DrawImage(imlCapas.Images["estrella"], 0, 0);
                        imlVistaArbol.Images.Add(".R" + nodoJuego.ImageKey, imagen);
                    }
                    arbolPadre.Nodes["juegos"].Nodes["numerosJugadores"].Nodes[juego.recomendados.ToString()].Nodes[juego.id].ImageKey = ".R" + nodoJuego.ImageKey;
                    arbolPadre.Nodes["juegos"].Nodes["numerosJugadores"].Nodes[juego.recomendados.ToString()].Nodes[juego.id].SelectedImageKey = ".R" + nodoJuego.ImageKey;
                }
                /* Clonamos el nodo del juego en los nodos padres de categorias a los que pertenezca */
                foreach (Enlace categoria in juego.categorias.Values)
                {
                    //Console.WriteLine("Desplegando juego en categoria: " + categoria.id);
                    arbolPadre.Nodes["juegos"].Nodes["categorias"].Nodes[categoria.id].Nodes.Add((TreeNode)nodoJuego.Clone());
                }
                /* Clonamos el nodo del juego en los nodos padres de familias a los que pertenezca */
                foreach (Enlace familia in juego.familias.Values)
                {
                    //Console.WriteLine("Desplegando juego en familia: " + familia.id);
                    arbolPadre.Nodes["juegos"].Nodes["familias"].Nodes[familia.id].Nodes.Add((TreeNode)nodoJuego.Clone());
                }
                /* Clonamos el nodo del juego en los nodos padres de mecanicas a los que pertenezca */
                foreach (Enlace mecanica in juego.mecanicas.Values)
                {
                    //Console.WriteLine("Desplegando juego en mecanica: " + mecanica.id);
                    arbolPadre.Nodes["juegos"].Nodes["mecanicas"].Nodes[mecanica.id].Nodes.Add((TreeNode)nodoJuego.Clone());
                }

                /* Desplegamos el nodo del juego en los nodos padres de juegos de rivales, solo si el jugo tiene rivales */
                //if (juego.rivales.Count > 0)
                {
                    arbolPadre.Nodes["rivales"].Nodes["juegosRivales"].Nodes.Add((TreeNode)nodoJuego.Clone());
                }
            }

            /* Colocamos los nuevos nodos en el arbol */
            //Console.WriteLine("Ordenando nodos
            arbolPadre.TreeViewNodeSorter = new OrdenadorNodos();
            arbolPadre.Sort();
            //Console.WriteLine("Agregando nodos");
            trvUsuario.Nodes.Add((TreeNode)arbolPadre.Nodes["autores"].Clone());
            trvUsuario.Nodes.Add((TreeNode)arbolPadre.Nodes["juegos"].Clone());
            trvUsuario.Nodes.Add((TreeNode)arbolPadre.Nodes["rivales"].Clone());
            trvUsuario.Nodes.Add((TreeNode)arbolPadre.Nodes["resumen"].Clone());
            //Console.WriteLine("Nodos agregados");
        }

        private void limpiarListaImagenes(ImageList listaImagenes)
        {
            /* Es mas eficiente respaldar las imagenes que se necesitan y meterlas de nuevo despues de usar el clear() que no borrarlas y borrar el resto selectivamente en un bucle */
            Image sinImagen = listaImagenes.Images["sinImagen"];
            Image autores = listaImagenes.Images["autores"];
            Image juegos = listaImagenes.Images["juegos"];
            Image numerosJugadores = listaImagenes.Images["numerosJugadores"];
            Image numeroJugadores = listaImagenes.Images["numeroJugadores"];
            Image categorias = listaImagenes.Images["categorias"];
            Image categoria = listaImagenes.Images["categoria"];
            Image familias = listaImagenes.Images["familias"];
            Image familia = listaImagenes.Images["familia"];
            Image mecanicas = listaImagenes.Images["mecanicas"];
            Image mecanica = listaImagenes.Images["mecanica"];
            Image rivales = listaImagenes.Images["rivales"];
            Image nombresRivales = listaImagenes.Images["nombresRivales"];
            Image rival = listaImagenes.Images["rival"];
            Image juegosRivales = listaImagenes.Images["juegosRivales"];
            Image resumen = listaImagenes.Images["resumen"];
            //Console.WriteLine("Limpiando imagenes");
            listaImagenes.Images.Clear();
            //Console.WriteLine("Imagenes limpiadas");
            listaImagenes.Images.Add("sinImagen", sinImagen);
            listaImagenes.Images.Add("autores", autores);
            listaImagenes.Images.Add("juegos", juegos);
            listaImagenes.Images.Add("numerosJugadores", numerosJugadores);
            listaImagenes.Images.Add("numeroJugadores", numeroJugadores);
            listaImagenes.Images.Add("categorias", categorias);
            listaImagenes.Images.Add("categoria", categoria);
            listaImagenes.Images.Add("familias", familias);
            listaImagenes.Images.Add("familia", familia);
            listaImagenes.Images.Add("mecanicas", mecanicas);
            listaImagenes.Images.Add("mecanica", mecanica);
            listaImagenes.Images.Add("rivales", rivales);
            listaImagenes.Images.Add("nombresRivales", nombresRivales);
            listaImagenes.Images.Add("rival", rival);
            listaImagenes.Images.Add("juegosRivales", juegosRivales);
            listaImagenes.Images.Add("resumen", resumen);
            /*Cargamos la extrella para cuando es favorito pero no tiene imagen*/
            Image imagen = listaImagenes.Images["sinImagen"];
            Graphics.FromImage(imagen).DrawImage(imlCapas.Images["estrella"], 0, 0);
            listaImagenes.Images.Add(".RsinImagen", imagen);
        }
        private void limpiarVistaLista()
        {
            limpiarListaImagenes(imlVistaLista);
            livUsuario.Clear();
            livUsuario.ShowGroups = false;
            Text = "BGG Finder";
            imlJuegosRivales.Images.Clear();
        }
        private void nodosItems(TreeNodeCollection nodos)
        {
            limpiarVistaLista();
            livUsuario.View = View.LargeIcon;

            ListViewItem[] listaItems = new ListViewItem[nodos.Count];
            for (int numeroNodo = 0; numeroNodo < nodos.Count; numeroNodo++)
            {

                listaItems[numeroNodo] = new ListViewItem(nodos[numeroNodo].Text, nodos[numeroNodo].ImageKey);
                listaItems[numeroNodo].Name = nodos[numeroNodo].Name;
                listaItems[numeroNodo].Tag = nodos[numeroNodo];

                if (nodos[numeroNodo].Tag.Equals("rival"))
                {
                    Image imagen = imlVistaLista.Images["rival"];
                    Graphics gr = Graphics.FromImage(imagen);
                    gr.DrawString(repertorio.rivales[nodos[numeroNodo].Name].victorias.ToString(), new Font("Arial", 16), new SolidBrush(Color.Green), 0, 0);
                    gr.DrawString(repertorio.rivales[nodos[numeroNodo].Name].derrotas.ToString(), new Font("Arial", 16), new SolidBrush(Color.Red), 80, 0);

                    listaItems[numeroNodo].ImageKey = nodos[numeroNodo].Name;
                    imlVistaLista.Images.Add(nodos[numeroNodo].Name, imagen);
                }
                else if (nodos[numeroNodo].ImageKey.StartsWith(".R") && !nodos[numeroNodo].ImageKey.StartsWith(".RsinImagen"))
                {
                    Image imagen = Image.FromFile(nodos[numeroNodo].ImageKey.Substring(2));
                    Graphics.FromImage(imagen).DrawImage(imlCapas.Images["estrella"], 0, 0);
                    imlVistaLista.Images.Add(nodos[numeroNodo].ImageKey, imagen);
                }
                else if (!imlVistaLista.Images.ContainsKey(nodos[numeroNodo].ImageKey))
                {
                    imlVistaLista.Images.Add(nodos[numeroNodo].ImageKey, Image.FromFile(nodos[numeroNodo].ImageKey));
                }
            }
            livUsuario.Items.AddRange(listaItems);
        }
        private void trvUsuario_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            nodoClickeado(e.Node);
        }
        private void livUsuario_DoubleClick(object sender, EventArgs e)
        {
            nodoClickeado((TreeNode)livUsuario.SelectedItems[0].Tag);
        }
        private void nodoClickeado(TreeNode nodo)
        {
            switch (nodo.Tag)
            {
                case "juego":
                    switch (nodo.Parent.Tag)
                    {
                        case "numeroJugadores":
                            limpiarVistaLista();
                            livUsuario.SmallImageList = imlVistaLista;
                            livUsuario.View = View.Details;

                            Juego juegoJugadores = repertorio.juegos[nodo.Name];
                            Text = "Numero De Jugadores : " + nodo.Parent.Name;

                            livUsuario.Columns.Add("Nombre Del Juego");
                            livUsuario.Columns.Add("Autores");
                            livUsuario.Columns.Add("Artistas");
                            livUsuario.Columns.Add("Rating");
                            livUsuario.Columns.Add("Publicación");
                            livUsuario.Columns.Add("Jugadas");
                            livUsuario.Columns.Add("Victorias");
                            livUsuario.Columns.Add("Derrotas");
                            livUsuario.Columns.Add("Empates");
                            livUsuario.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                            livUsuario.Columns[0].Width = 250;
                            livUsuario.Columns[1].Width = 125;
                            livUsuario.Columns[2].Width = 125;

                            ListViewItem ItemsNumeroJugadores = new ListViewItem(juegoJugadores.nombre, "sinImagen");
                            ItemsNumeroJugadores.Tag = nodo.Parent;

                            ItemsNumeroJugadores.SubItems.Add(juegoJugadores.cadenaAutores);
                            ItemsNumeroJugadores.SubItems.Add(juegoJugadores.cadenaArtistas);
                            ItemsNumeroJugadores.SubItems.Add(juegoJugadores.rating);
                            ItemsNumeroJugadores.SubItems.Add(juegoJugadores.añoPublicación);
                            ItemsNumeroJugadores.SubItems.Add(juegoJugadores.numerosJuegadores[int.Parse(nodo.Parent.Name)].jugadas.ToString());
                            ItemsNumeroJugadores.SubItems.Add(juegoJugadores.numerosJuegadores[int.Parse(nodo.Parent.Name)].victorias.ToString());
                            ItemsNumeroJugadores.SubItems.Add(juegoJugadores.numerosJuegadores[int.Parse(nodo.Parent.Name)].derrotas.ToString());
                            ItemsNumeroJugadores.SubItems.Add(juegoJugadores.numerosJuegadores[int.Parse(nodo.Parent.Name)].empates.ToString());

                            /*
                             * No le pongo estrella si es un numero de jugadores recomendado
                             * Y ADEMAS 
                             * Sin imagen le tengo que poner estrella de igual manera, falta ese else
                             */
                            if (File.Exists(directorioImagenesJuegos + "/" + juegoJugadores.id + ".jpg"))
                            {
                                imlVistaLista.Images.Add(Image.FromFile(directorioImagenesJuegos + "/" + juegoJugadores.id + ".jpg"));
                                ItemsNumeroJugadores.ImageIndex = imlVistaLista.Images.Count - 1;
                            }

                            livUsuario.Items.Add(ItemsNumeroJugadores);
                            break;
                        case "juegosRivales":
                            limpiarVistaLista();
                            livUsuario.SmallImageList = null;
                            /* Cambiando a una de vista es la unicamanera en la que se cambia el alto de las filas una vez quitas las lista de imagenes
                             * y para eso forzamos el cambio de tipo de vista ya podria no cambiar al tener la primera vez View.Details*/
                            livUsuario.View = View.LargeIcon;
                            livUsuario.View = View.Details;

                            Juego juegoRivales = repertorio.juegos[nodo.Name];
                            Text = "Juego : " + juegoRivales.nombre;

                            livUsuario.Columns.Add("Nombre Del Rival");
                            livUsuario.Columns.Add("Jugadas");
                            livUsuario.Columns.Add("Victorias");
                            livUsuario.Columns.Add("Derrotas");
                            livUsuario.Columns.Add("Empates");
                            livUsuario.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                            ListViewItem[] arregloItems = new ListViewItem[juegoRivales.rivales.Count];
                            int indiceRival = 0;
                            foreach (Rival rivalJuego in juegoRivales.rivales.Values)
                            {
                                arregloItems[indiceRival] = new ListViewItem(rivalJuego.nombre);
                                arregloItems[indiceRival].Name = rivalJuego.nombre.ToLower();
                                arregloItems[indiceRival].Tag = nodo.Parent;

                                arregloItems[indiceRival].SubItems.Add(rivalJuego.jugadas[nodo.Name].jugadas.ToString());
                                arregloItems[indiceRival].SubItems.Add(rivalJuego.jugadas[nodo.Name].victorias.ToString());
                                arregloItems[indiceRival].SubItems.Add(rivalJuego.jugadas[nodo.Name].derrotas.ToString());
                                arregloItems[indiceRival].SubItems.Add(rivalJuego.jugadas[nodo.Name].empates.ToString());
                                indiceRival++;
                            }

                            livUsuario.Items.AddRange(arregloItems);
                            break;
                        default:
                            limpiarVistaLista();
                            livUsuario.SmallImageList = imlVistaLista;
                            livUsuario.View = View.Details;

                            Juego juego = repertorio.juegos[nodo.Name];

                            livUsuario.Columns.Add("Nombre Del Juego");
                            livUsuario.Columns.Add("Autores");
                            livUsuario.Columns.Add("Artistas");
                            livUsuario.Columns.Add("Rating");
                            livUsuario.Columns.Add("Publicación");
                            livUsuario.Columns.Add("Jugadas");
                            livUsuario.Columns.Add("Victorias");
                            livUsuario.Columns.Add("Derrotas");
                            livUsuario.Columns.Add("Empates");
                            livUsuario.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                            livUsuario.Columns[0].Width = 250;
                            livUsuario.Columns[1].Width = 125;
                            livUsuario.Columns[2].Width = 125;

                            ListViewItem itemFila = new ListViewItem(juego.nombre, "sinImagen");
                            itemFila.Tag = nodo.Parent;

                            itemFila.SubItems.Add(juego.cadenaAutores);
                            itemFila.SubItems.Add(juego.cadenaArtistas);
                            itemFila.SubItems.Add(juego.rating);
                            itemFila.SubItems.Add(juego.añoPublicación);
                            itemFila.SubItems.Add(juego.jugadas.ToString());
                            itemFila.SubItems.Add(juego.victorias.ToString());
                            itemFila.SubItems.Add(juego.derrotas.ToString());
                            itemFila.SubItems.Add(juego.empates.ToString());

                            if (File.Exists(directorioImagenesJuegos + "/" + juego.id + ".jpg"))
                            {
                                imlVistaLista.Images.Add(Image.FromFile(directorioImagenesJuegos + "/" + juego.id + ".jpg"));
                                itemFila.ImageIndex = imlVistaLista.Images.Count - 1;
                            }

                            livUsuario.Items.Add(itemFila);
                            break;
                    }
                    break;
                case "rival":
                    limpiarVistaLista();
                    livUsuario.SmallImageList = imlJuegosRivales;
                    //livUsuario.View = View.LargeIcon;
                    livUsuario.View = View.Details;

                    Rival rival = repertorio.rivales[nodo.Name];
                    Text = "Rival : " + rival.nombre;

                    livUsuario.Columns.Add("Nombre Del Juego");
                    livUsuario.Columns.Add("Jugadas");
                    livUsuario.Columns.Add("Victorias");
                    livUsuario.Columns.Add("Derrotas");
                    livUsuario.Columns.Add("Empates");
                    livUsuario.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    livUsuario.Columns[0].Width = 250;

                    ListViewItem[] arregloItemsJugadas = new ListViewItem[rival.jugadas.Count];
                    int indiceJugada = 0;
                    foreach (Jugada jugada in rival.jugadas.Values)
                    {
                        if (File.Exists(directorioImagenesJuegos + "/" + jugada.juego.id + ".jpg"))
                        {
                            imlJuegosRivales.Images.Add(jugada.juego.id, Image.FromFile(directorioImagenesJuegos + "/" + jugada.juego.id + ".jpg"));
                        }
                        arregloItemsJugadas[indiceJugada] = new ListViewItem(jugada.juego.nombre, jugada.juego.id);
                        arregloItemsJugadas[indiceJugada].Name = jugada.juego.id;
                        arregloItemsJugadas[indiceJugada].Tag = nodo.Parent;

                        arregloItemsJugadas[indiceJugada].SubItems.Add(jugada.jugadas.ToString());
                        arregloItemsJugadas[indiceJugada].SubItems.Add(jugada.victorias.ToString());
                        arregloItemsJugadas[indiceJugada].SubItems.Add(jugada.derrotas.ToString());
                        arregloItemsJugadas[indiceJugada].SubItems.Add(jugada.empates.ToString());
                        indiceJugada++;
                    }

                    livUsuario.Items.AddRange(arregloItemsJugadas);
                    break;
                case "resumen":
                    if( repertorio == null)
                    {
                        break;
                    }
                    limpiarVistaLista();
                    livUsuario.View = View.LargeIcon;
                    livUsuario.ShowGroups = true;
                    if (repertorio.autorPreferido != null)
                    {
                        String identificador = ".autorPreferido" + repertorio.autorPreferido.id;
                        livUsuario.Items.Add(identificador, repertorio.autorPreferido.valor, "sinImagen");
                        livUsuario.Items[identificador].Group = livUsuario.Groups["autorPreferido"];
                        livUsuario.Items[identificador].Tag = trvUsuario.Nodes["autores"].Nodes[repertorio.autorPreferido.id];
                        String rutaImagen = directorioImagenesAutores + "/" + repertorio.autorPreferido.id + ".jpg";
                        if (File.Exists(rutaImagen))
                        {
                            imlVistaLista.Images.Add(identificador, Image.FromFile(rutaImagen));
                            livUsuario.Items[identificador].ImageKey = identificador;
                        }
                    }
                    if (repertorio.juegoMasJugado != null)
                    {
                        String identificador = ".juegoMasJugado" + repertorio.juegoMasJugado.id;
                        livUsuario.Items.Add(identificador, repertorio.juegoMasJugado.nombre, "sinImagen");
                        livUsuario.Items[identificador].Group = livUsuario.Groups["juegoMasJugado"];
                        livUsuario.Items[identificador].Tag = trvUsuario.Nodes["rivales"].Nodes["juegosRivales"].Nodes[repertorio.juegoMasJugado.id];
                        String rutaImagen = directorioImagenesJuegos + "/" + repertorio.juegoMasJugado.id + ".jpg";
                        if (File.Exists(rutaImagen))
                        {
                            imlVistaLista.Images.Add(identificador, Image.FromFile(rutaImagen));
                            livUsuario.Items[identificador].ImageKey = identificador;
                        }
                    }
                    if (repertorio.juegoMenosJugado != null)
                    {
                        String identificador = ".juegoMenosJugado" + repertorio.juegoMenosJugado.id;
                        livUsuario.Items.Add(identificador, repertorio.juegoMenosJugado.nombre, "sinImagen");
                        livUsuario.Items[identificador].Group = livUsuario.Groups["juegoMenosJugado"];
                        livUsuario.Items[identificador].Tag = trvUsuario.Nodes["rivales"].Nodes["juegosRivales"].Nodes[repertorio.juegoMenosJugado.id];
                        String rutaImagen = directorioImagenesJuegos + "/" + repertorio.juegoMenosJugado.id + ".jpg";
                        if (File.Exists(rutaImagen))
                        {
                            imlVistaLista.Images.Add(identificador, Image.FromFile(rutaImagen));
                            livUsuario.Items[identificador].ImageKey = identificador;
                        }
                    }
                    if (repertorio.juegoUltimoJugado != null)
                    {
                        String identificador = ".juegoUltimoJugado" + repertorio.juegoUltimoJugado.id;
                        livUsuario.Items.Add(identificador, repertorio.juegoUltimoJugado.nombre, "sinImagen");
                        livUsuario.Items[identificador].Group = livUsuario.Groups["juegoJugadoRecientemente"];
                        livUsuario.Items[identificador].Tag = trvUsuario.Nodes["rivales"].Nodes["juegosRivales"].Nodes[repertorio.juegoUltimoJugado.id];
                        String rutaImagen = directorioImagenesJuegos + "/" + repertorio.juegoUltimoJugado.id + ".jpg";
                        if (File.Exists(rutaImagen))
                        {
                            imlVistaLista.Images.Add(identificador, Image.FromFile(rutaImagen));
                            livUsuario.Items[identificador].ImageKey = identificador;
                        }
                    }
                    if (repertorio.juegoMuchoSinJugar != null)
                    {
                        String identificador = ".juegoMuchoSinJugar" + repertorio.juegoMuchoSinJugar.id;
                        livUsuario.Items.Add(identificador, repertorio.juegoMuchoSinJugar.nombre, "sinImagen");
                        livUsuario.Items[identificador].Group = livUsuario.Groups["juegoConMasTiempoSinJugar"];
                        livUsuario.Items[identificador].Tag = trvUsuario.Nodes["rivales"].Nodes["juegosRivales"].Nodes[repertorio.juegoMuchoSinJugar.id];
                        String rutaImagen = directorioImagenesJuegos + "/" + repertorio.juegoMuchoSinJugar.id + ".jpg";
                        if (File.Exists(rutaImagen))
                        {
                            imlVistaLista.Images.Add(identificador, Image.FromFile(rutaImagen));
                            livUsuario.Items[identificador].ImageKey = identificador;
                        }
                    }
                    if (repertorio.rivalMasGanado != null)
                    {
                        String identificador = ".rivalMasGanado" + repertorio.rivalMasGanado.nombre;
                        livUsuario.Items.Add(identificador, repertorio.rivalMasGanado.nombre, identificador);
                        livUsuario.Items[identificador].Group = livUsuario.Groups["rivalContraElQueMasAGanado"];
                        livUsuario.Items[identificador].Tag = trvUsuario.Nodes["rivales"].Nodes["nombresRivales"].Nodes[repertorio.rivalMasGanado.nombre];
                        Image imagen = imlVistaLista.Images["rival"];
                        Graphics gr = Graphics.FromImage(imagen);
                        gr.DrawString(repertorio.rivalMasGanado.victorias.ToString(), new Font("Arial", 16), new SolidBrush(Color.Green), 0, 0);
                        gr.DrawString(repertorio.rivalMasGanado.derrotas.ToString(), new Font("Arial", 16), new SolidBrush(Color.Red), 80, 0);
                        imlVistaLista.Images.Add(identificador, imagen);
                    }
                    if (repertorio.rivalMasPerdido != null)
                    {
                        String identificador = ".rivalMasPerdido" + repertorio.rivalMasPerdido.nombre;
                        livUsuario.Items.Add(identificador, repertorio.rivalMasPerdido.nombre, identificador);
                        livUsuario.Items[identificador].Group = livUsuario.Groups["rivalContraElQueMasAPerdido"];
                        livUsuario.Items[identificador].Tag = trvUsuario.Nodes["rivales"].Nodes["nombresRivales"].Nodes[repertorio.rivalMasPerdido.nombre];
                        Image imagen = imlVistaLista.Images["rival"];
                        Graphics gr = Graphics.FromImage(imagen);
                        gr.DrawString(repertorio.rivalMasPerdido.victorias.ToString(), new Font("Arial", 16), new SolidBrush(Color.Green), 0, 0);
                        gr.DrawString(repertorio.rivalMasPerdido.derrotas.ToString(), new Font("Arial", 16), new SolidBrush(Color.Red), 80, 0);
                        imlVistaLista.Images.Add(identificador, imagen);
                    }
                    break;
                default:
                    nodosItems(nodo.Nodes);
                    break;
            }
        }

        private void txtNombreUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscarUsuario_Click(sender, e);
            }
        }
    }
}
