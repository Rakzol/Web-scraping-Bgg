namespace BGG_Finder
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Autores");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("# Jugadores");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Categorías");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Familias");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Mecánicas");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Juegos", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Nombres");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Juegos");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Rivales", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Resumen");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Autor Preferido", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Juego Más Jugado", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Juego Menos Jugado", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Juego Jugado Recientemente", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Juego Con Más Tiempo Sin Jugar", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Rival Contra El Que Más Ha Ganado", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("Rival Contra El Que Más Ha Perdido", System.Windows.Forms.HorizontalAlignment.Center);
            this.trvUsuario = new System.Windows.Forms.TreeView();
            this.imlVistaArbol = new System.Windows.Forms.ImageList(this.components);
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.btnBuscarUsuario = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.livUsuario = new System.Windows.Forms.ListView();
            this.imlVistaLista = new System.Windows.Forms.ImageList(this.components);
            this.imlCapas = new System.Windows.Forms.ImageList(this.components);
            this.chkActualizarColeccion = new System.Windows.Forms.CheckBox();
            this.progresoCarga = new System.Windows.Forms.ProgressBar();
            this.chkActualizarJugadas = new System.Windows.Forms.CheckBox();
            this.imlJuegosRivales = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // trvUsuario
            // 
            this.trvUsuario.ImageIndex = 0;
            this.trvUsuario.ImageList = this.imlVistaArbol;
            this.trvUsuario.Location = new System.Drawing.Point(12, 41);
            this.trvUsuario.Name = "trvUsuario";
            treeNode1.ImageKey = "autores";
            treeNode1.Name = "autores";
            treeNode1.SelectedImageKey = "autores";
            treeNode1.Tag = "autores";
            treeNode1.Text = "Autores";
            treeNode2.ImageKey = "numerosJugadores";
            treeNode2.Name = "numerosJugadores";
            treeNode2.SelectedImageKey = "numerosJugadores";
            treeNode2.Tag = "numerosJugadores";
            treeNode2.Text = "# Jugadores";
            treeNode3.ImageKey = "categorias";
            treeNode3.Name = "categorias";
            treeNode3.SelectedImageKey = "categorias";
            treeNode3.Tag = "categorias";
            treeNode3.Text = "Categorías";
            treeNode4.ImageKey = "familias";
            treeNode4.Name = "familias";
            treeNode4.SelectedImageKey = "familias";
            treeNode4.Tag = "familias";
            treeNode4.Text = "Familias";
            treeNode5.ImageKey = "mecanicas";
            treeNode5.Name = "mecanicas";
            treeNode5.SelectedImageKey = "mecanicas";
            treeNode5.Tag = "mecanicas";
            treeNode5.Text = "Mecánicas";
            treeNode6.ImageKey = "juegos";
            treeNode6.Name = "juegos";
            treeNode6.SelectedImageKey = "juegos";
            treeNode6.Tag = "juegos";
            treeNode6.Text = "Juegos";
            treeNode7.ImageKey = "nombresRivales";
            treeNode7.Name = "nombresRivales";
            treeNode7.SelectedImageKey = "nombresRivales";
            treeNode7.Tag = "nombresRivales";
            treeNode7.Text = "Nombres";
            treeNode8.ImageKey = "juegosRivales";
            treeNode8.Name = "juegosRivales";
            treeNode8.SelectedImageKey = "juegosRivales";
            treeNode8.Tag = "juegosRivales";
            treeNode8.Text = "Juegos";
            treeNode9.ImageKey = "rivales";
            treeNode9.Name = "rivales";
            treeNode9.SelectedImageKey = "rivales";
            treeNode9.Tag = "rivales";
            treeNode9.Text = "Rivales";
            treeNode10.ImageKey = "resumen";
            treeNode10.Name = "resumen";
            treeNode10.SelectedImageKey = "resumen";
            treeNode10.Tag = "resumen";
            treeNode10.Text = "Resumen";
            this.trvUsuario.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode6,
            treeNode9,
            treeNode10});
            this.trvUsuario.SelectedImageIndex = 0;
            this.trvUsuario.Size = new System.Drawing.Size(204, 397);
            this.trvUsuario.TabIndex = 0;
            this.trvUsuario.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvUsuario_NodeMouseClick);
            // 
            // imlVistaArbol
            // 
            this.imlVistaArbol.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlVistaArbol.ImageStream")));
            this.imlVistaArbol.TransparentColor = System.Drawing.Color.Transparent;
            this.imlVistaArbol.Images.SetKeyName(0, "sinImagen");
            this.imlVistaArbol.Images.SetKeyName(1, "autores");
            this.imlVistaArbol.Images.SetKeyName(2, "juegos");
            this.imlVistaArbol.Images.SetKeyName(3, "numerosJugadores");
            this.imlVistaArbol.Images.SetKeyName(4, "numeroJugadores");
            this.imlVistaArbol.Images.SetKeyName(5, "categorias");
            this.imlVistaArbol.Images.SetKeyName(6, "categoria");
            this.imlVistaArbol.Images.SetKeyName(7, "familias");
            this.imlVistaArbol.Images.SetKeyName(8, "familia");
            this.imlVistaArbol.Images.SetKeyName(9, "mecanicas");
            this.imlVistaArbol.Images.SetKeyName(10, "mecanica");
            this.imlVistaArbol.Images.SetKeyName(11, "rivales");
            this.imlVistaArbol.Images.SetKeyName(12, "nombresRivales");
            this.imlVistaArbol.Images.SetKeyName(13, "rival");
            this.imlVistaArbol.Images.SetKeyName(14, "juegosRivales");
            this.imlVistaArbol.Images.SetKeyName(15, "resumen");
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.Location = new System.Drawing.Point(116, 14);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(100, 20);
            this.txtNombreUsuario.TabIndex = 1;
            this.txtNombreUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreUsuario_KeyDown);
            // 
            // btnBuscarUsuario
            // 
            this.btnBuscarUsuario.Location = new System.Drawing.Point(222, 12);
            this.btnBuscarUsuario.Name = "btnBuscarUsuario";
            this.btnBuscarUsuario.Size = new System.Drawing.Size(52, 23);
            this.btnBuscarUsuario.TabIndex = 2;
            this.btnBuscarUsuario.Text = "Buscar";
            this.btnBuscarUsuario.UseVisualStyleBackColor = true;
            this.btnBuscarUsuario.Click += new System.EventHandler(this.btnBuscarUsuario_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nombre de Usuario";
            // 
            // livUsuario
            // 
            listViewGroup1.Header = "Autor Preferido";
            listViewGroup1.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup1.Name = "autorPreferido";
            listViewGroup2.Header = "Juego Más Jugado";
            listViewGroup2.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup2.Name = "juegoMasJugado";
            listViewGroup3.Header = "Juego Menos Jugado";
            listViewGroup3.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup3.Name = "juegoMenosJugado";
            listViewGroup4.Header = "Juego Jugado Recientemente";
            listViewGroup4.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup4.Name = "juegoJugadoRecientemente";
            listViewGroup5.Header = "Juego Con Más Tiempo Sin Jugar";
            listViewGroup5.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup5.Name = "juegoConMasTiempoSinJugar";
            listViewGroup6.Header = "Rival Contra El Que Más Ha Ganado";
            listViewGroup6.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup6.Name = "rivalContraElQueMasAGanado";
            listViewGroup7.Header = "Rival Contra El Que Más Ha Perdido";
            listViewGroup7.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup7.Name = "rivalContraElQueMasAPerdido";
            this.livUsuario.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4,
            listViewGroup5,
            listViewGroup6,
            listViewGroup7});
            this.livUsuario.HideSelection = false;
            this.livUsuario.LargeImageList = this.imlVistaLista;
            this.livUsuario.Location = new System.Drawing.Point(222, 41);
            this.livUsuario.Name = "livUsuario";
            this.livUsuario.Size = new System.Drawing.Size(604, 397);
            this.livUsuario.SmallImageList = this.imlJuegosRivales;
            this.livUsuario.TabIndex = 5;
            this.livUsuario.UseCompatibleStateImageBehavior = false;
            this.livUsuario.DoubleClick += new System.EventHandler(this.livUsuario_DoubleClick);
            // 
            // imlVistaLista
            // 
            this.imlVistaLista.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlVistaLista.ImageStream")));
            this.imlVistaLista.TransparentColor = System.Drawing.Color.Transparent;
            this.imlVistaLista.Images.SetKeyName(0, "sinImagen");
            this.imlVistaLista.Images.SetKeyName(1, "autores");
            this.imlVistaLista.Images.SetKeyName(2, "juegos");
            this.imlVistaLista.Images.SetKeyName(3, "numerosJugadores");
            this.imlVistaLista.Images.SetKeyName(4, "numeroJugadores");
            this.imlVistaLista.Images.SetKeyName(5, "categorias");
            this.imlVistaLista.Images.SetKeyName(6, "categoria");
            this.imlVistaLista.Images.SetKeyName(7, "familias");
            this.imlVistaLista.Images.SetKeyName(8, "familia");
            this.imlVistaLista.Images.SetKeyName(9, "mecanicas");
            this.imlVistaLista.Images.SetKeyName(10, "mecanica");
            this.imlVistaLista.Images.SetKeyName(11, "rivales");
            this.imlVistaLista.Images.SetKeyName(12, "nombresRivales");
            this.imlVistaLista.Images.SetKeyName(13, "rival");
            this.imlVistaLista.Images.SetKeyName(14, "juegosRivales");
            this.imlVistaLista.Images.SetKeyName(15, "resumen");
            // 
            // imlCapas
            // 
            this.imlCapas.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlCapas.ImageStream")));
            this.imlCapas.TransparentColor = System.Drawing.Color.Transparent;
            this.imlCapas.Images.SetKeyName(0, "estrella");
            // 
            // chkActualizarColeccion
            // 
            this.chkActualizarColeccion.AutoSize = true;
            this.chkActualizarColeccion.Location = new System.Drawing.Point(280, 16);
            this.chkActualizarColeccion.Name = "chkActualizarColeccion";
            this.chkActualizarColeccion.Size = new System.Drawing.Size(122, 17);
            this.chkActualizarColeccion.TabIndex = 6;
            this.chkActualizarColeccion.Text = "Actualizar Colección";
            this.chkActualizarColeccion.UseVisualStyleBackColor = true;
            // 
            // progresoCarga
            // 
            this.progresoCarga.Location = new System.Drawing.Point(529, 12);
            this.progresoCarga.Name = "progresoCarga";
            this.progresoCarga.Size = new System.Drawing.Size(297, 23);
            this.progresoCarga.TabIndex = 7;
            this.progresoCarga.Visible = false;
            // 
            // chkActualizarJugadas
            // 
            this.chkActualizarJugadas.AutoSize = true;
            this.chkActualizarJugadas.Location = new System.Drawing.Point(408, 16);
            this.chkActualizarJugadas.Name = "chkActualizarJugadas";
            this.chkActualizarJugadas.Size = new System.Drawing.Size(115, 17);
            this.chkActualizarJugadas.TabIndex = 8;
            this.chkActualizarJugadas.Text = "Actualizar Jugadas";
            this.chkActualizarJugadas.UseVisualStyleBackColor = true;
            // 
            // imlJuegosRivales
            // 
            this.imlJuegosRivales.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
            this.imlJuegosRivales.ImageSize = new System.Drawing.Size(64, 64);
            this.imlJuegosRivales.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 450);
            this.Controls.Add(this.chkActualizarJugadas);
            this.Controls.Add(this.progresoCarga);
            this.Controls.Add(this.chkActualizarColeccion);
            this.Controls.Add(this.livUsuario);
            this.Controls.Add(this.trvUsuario);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBuscarUsuario);
            this.Controls.Add(this.txtNombreUsuario);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrincipal";
            this.Text = "BGG Finder";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvUsuario;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.Button btnBuscarUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView livUsuario;
        private System.Windows.Forms.ImageList imlVistaArbol;
        private System.Windows.Forms.ImageList imlVistaLista;
        private System.Windows.Forms.ImageList imlCapas;
        private System.Windows.Forms.CheckBox chkActualizarColeccion;
        private System.Windows.Forms.ProgressBar progresoCarga;
        private System.Windows.Forms.CheckBox chkActualizarJugadas;
        private System.Windows.Forms.ImageList imlJuegosRivales;
    }
}

