<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SynctoCloud
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SynctoCloud))
        Me.BackgroundWorkerSYNCTOCLOUD = New System.ComponentModel.BackgroundWorker()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ButtonSYNCDATA = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewEXPDET = New System.Windows.Forms.DataGridView()
        Me.Column99 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column100 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column101 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column102 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column103 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column104 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column105 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column106 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column107 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column108 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column109 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column110 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column111 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column112 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column113 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column114 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelTTLRowtoSync = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.DataGridViewINV = New System.Windows.Forms.DataGridView()
        Me.Column165 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column166 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column167 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column168 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column169 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column170 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column171 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column172 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column173 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column174 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column175 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column176 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column177 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column178 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column179 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewEXP = New System.Windows.Forms.DataGridView()
        Me.Column59 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column60 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column61 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column62 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column63 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column64 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column65 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column66 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column67 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column68 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column69 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column70 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column71 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTRAN = New System.Windows.Forms.DataGridView()
        Me.ButtonSYNCINVENTORY = New System.Windows.Forms.Button()
        Me.LabelZREADINVITEM = New System.Windows.Forms.Label()
        Me.LabelZREADINVTIME = New System.Windows.Forms.Label()
        Me.LabelZREADINV = New System.Windows.Forms.Label()
        Me.LabelErrorItem = New System.Windows.Forms.Label()
        Me.LabelErrorTime = New System.Windows.Forms.Label()
        Me.LabelError = New System.Windows.Forms.Label()
        Me.LabelCouponItem = New System.Windows.Forms.Label()
        Me.LabelCouponTime = New System.Windows.Forms.Label()
        Me.LabelCoupon = New System.Windows.Forms.Label()
        Me.LabelPRICEREQItem = New System.Windows.Forms.Label()
        Me.LabelPRICEREQTime = New System.Windows.Forms.Label()
        Me.LabelPRICEREQ = New System.Windows.Forms.Label()
        Me.LabelDEPOSITItem = New System.Windows.Forms.Label()
        Me.LabelDEPOSITTime = New System.Windows.Forms.Label()
        Me.LabelDEPOSIT = New System.Windows.Forms.Label()
        Me.LabelMODETItem = New System.Windows.Forms.Label()
        Me.LabelMODETTime = New System.Windows.Forms.Label()
        Me.LabelMODET = New System.Windows.Forms.Label()
        Me.LabelCPRODItem = New System.Windows.Forms.Label()
        Me.LabelCPRODTime = New System.Windows.Forms.Label()
        Me.LabelCPROD = New System.Windows.Forms.Label()
        Me.LabelRETItem = New System.Windows.Forms.Label()
        Me.LabelRETTime = New System.Windows.Forms.Label()
        Me.LabelRET = New System.Windows.Forms.Label()
        Me.LabelSYS1Item = New System.Windows.Forms.Label()
        Me.LabelACCItem = New System.Windows.Forms.Label()
        Me.LabelEXPDItem = New System.Windows.Forms.Label()
        Me.LabelEXPItem = New System.Windows.Forms.Label()
        Me.LabelINVItem = New System.Windows.Forms.Label()
        Me.LabelDTransactDItem = New System.Windows.Forms.Label()
        Me.LabelDTransacItem = New System.Windows.Forms.Label()
        Me.LabelSYS1Time = New System.Windows.Forms.Label()
        Me.LabelSYS1 = New System.Windows.Forms.Label()
        Me.LabelACCTime = New System.Windows.Forms.Label()
        Me.LabelEXPDTime = New System.Windows.Forms.Label()
        Me.LabelEXPTime = New System.Windows.Forms.Label()
        Me.LabelINVTime = New System.Windows.Forms.Label()
        Me.LabelDTransactDTime = New System.Windows.Forms.Label()
        Me.LabelDTransacTime = New System.Windows.Forms.Label()
        Me.LabelACC = New System.Windows.Forms.Label()
        Me.LabelEXPD = New System.Windows.Forms.Label()
        Me.LabelEXP = New System.Windows.Forms.Label()
        Me.LabelINV = New System.Windows.Forms.Label()
        Me.LabelDTransactD = New System.Windows.Forms.Label()
        Me.LabelDTransac = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DataGridViewZREADINVENTORY = New System.Windows.Forms.DataGridView()
        Me.Column116 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column117 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column118 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column119 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column120 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column121 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column122 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column123 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column124 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column125 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column126 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column127 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column128 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column129 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column130 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column131 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column132 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DatagridviewSenior = New System.Windows.Forms.DataGridView()
        Me.DataGridViewERRORS = New System.Windows.Forms.DataGridView()
        Me.DataGridViewCoupons = New System.Windows.Forms.DataGridView()
        Me.LabelTime = New System.Windows.Forms.Label()
        Me.LabelRowtoSync = New System.Windows.Forms.Label()
        Me.DataGridViewPriceChangeRequest = New System.Windows.Forms.DataGridView()
        Me.DataGridViewDepositSlip = New System.Windows.Forms.DataGridView()
        Me.Column31 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column32 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column33 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column34 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column35 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column36 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column37 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column38 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column39 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column40 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column41 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewMODEOFTRANSACTION = New System.Windows.Forms.DataGridView()
        Me.Column88 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column89 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column90 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column91 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column92 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column93 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column94 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column95 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column96 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column97 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column98 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewRetrefdetails = New System.Windows.Forms.DataGridView()
        Me.Column155 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column156 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column157 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column158 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column159 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column160 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column161 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column162 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column163 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column164 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewSYSLOG2 = New System.Windows.Forms.DataGridView()
        Me.Column17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column181 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewLocusers = New System.Windows.Forms.DataGridView()
        Me.Column72 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column73 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column74 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column75 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column76 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column77 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column78 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column79 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column80 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column81 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column82 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column83 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column84 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column85 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column86 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column87 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewSYSLOG1 = New System.Windows.Forms.DataGridView()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column180 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTRANDET = New System.Windows.Forms.DataGridView()
        Me.Column137 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column115 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column138 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column139 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column140 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column141 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column142 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column143 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column144 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column145 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column146 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column147 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column148 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column149 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column150 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column151 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column152 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column153 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column154 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCUSTOMPRODUCTS = New System.Windows.Forms.DataGridView()
        Me.Column42 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column43 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column44 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column45 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column46 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column47 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column48 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column49 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column50 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column51 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column52 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column53 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column54 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column55 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column56 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column57 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column58 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewSYSLOG4 = New System.Windows.Forms.DataGridView()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column183 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewSYSLOG3 = New System.Windows.Forms.DataGridView()
        Me.Column24 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column25 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column26 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column27 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column28 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column29 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column30 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column182 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.BackgroundWorkerFILLDATAGRIDS = New System.ComponentModel.BackgroundWorker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelSeniorDetailsItem = New System.Windows.Forms.Label()
        Me.LabelSeniorDetailsTime = New System.Windows.Forms.Label()
        Me.LabelSeniorDetails = New System.Windows.Forms.Label()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewEXPDET, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        CType(Me.DataGridViewINV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewEXP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewTRAN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridViewZREADINVENTORY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DatagridviewSenior, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewERRORS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewCoupons, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewPriceChangeRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewDepositSlip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewMODEOFTRANSACTION, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewRetrefdetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewSYSLOG2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewLocusers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewSYSLOG1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewTRANDET, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewCUSTOMPRODUCTS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewSYSLOG4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewSYSLOG3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BackgroundWorkerSYNCTOCLOUD
        '
        Me.BackgroundWorkerSYNCTOCLOUD.WorkerReportsProgress = True
        Me.BackgroundWorkerSYNCTOCLOUD.WorkerSupportsCancellation = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'ButtonSYNCDATA
        '
        Me.ButtonSYNCDATA.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.ButtonSYNCDATA.FlatAppearance.BorderSize = 0
        Me.ButtonSYNCDATA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSYNCDATA.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSYNCDATA.ForeColor = System.Drawing.Color.White
        Me.ButtonSYNCDATA.Location = New System.Drawing.Point(506, 146)
        Me.ButtonSYNCDATA.Name = "ButtonSYNCDATA"
        Me.ButtonSYNCDATA.Size = New System.Drawing.Size(228, 41)
        Me.ButtonSYNCDATA.TabIndex = 25
        Me.ButtonSYNCDATA.Text = "SYNC"
        Me.ButtonSYNCDATA.UseVisualStyleBackColor = False
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AllowUserToResizeColumns = False
        Me.DataGridView2.AllowUserToResizeRows = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2})
        Me.DataGridView2.Location = New System.Drawing.Point(6, 19)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(230, 80)
        Me.DataGridView2.TabIndex = 19
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Column2"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        Me.DataGridView1.Location = New System.Drawing.Point(6, 19)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(230, 80)
        Me.DataGridView1.TabIndex = 18
        '
        'Column1
        '
        Me.Column1.HeaderText = "Column1"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Column2"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'DataGridViewEXPDET
        '
        Me.DataGridViewEXPDET.AllowUserToAddRows = False
        Me.DataGridViewEXPDET.AllowUserToDeleteRows = False
        Me.DataGridViewEXPDET.AllowUserToResizeColumns = False
        Me.DataGridViewEXPDET.AllowUserToResizeRows = False
        Me.DataGridViewEXPDET.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewEXPDET.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column99, Me.Column100, Me.Column101, Me.Column102, Me.Column103, Me.Column104, Me.Column105, Me.Column106, Me.Column107, Me.Column108, Me.Column109, Me.Column110, Me.Column111, Me.Column112, Me.Column113, Me.Column114})
        Me.DataGridViewEXPDET.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewEXPDET.Name = "DataGridViewEXPDET"
        Me.DataGridViewEXPDET.ReadOnly = True
        Me.DataGridViewEXPDET.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewEXPDET.TabIndex = 12
        '
        'Column99
        '
        Me.Column99.HeaderText = "Column99"
        Me.Column99.Name = "Column99"
        Me.Column99.ReadOnly = True
        '
        'Column100
        '
        Me.Column100.HeaderText = "Column100"
        Me.Column100.Name = "Column100"
        Me.Column100.ReadOnly = True
        '
        'Column101
        '
        Me.Column101.HeaderText = "Column101"
        Me.Column101.Name = "Column101"
        Me.Column101.ReadOnly = True
        '
        'Column102
        '
        Me.Column102.HeaderText = "Column102"
        Me.Column102.Name = "Column102"
        Me.Column102.ReadOnly = True
        '
        'Column103
        '
        Me.Column103.HeaderText = "Column103"
        Me.Column103.Name = "Column103"
        Me.Column103.ReadOnly = True
        '
        'Column104
        '
        Me.Column104.HeaderText = "Column104"
        Me.Column104.Name = "Column104"
        Me.Column104.ReadOnly = True
        '
        'Column105
        '
        Me.Column105.HeaderText = "Column105"
        Me.Column105.Name = "Column105"
        Me.Column105.ReadOnly = True
        '
        'Column106
        '
        Me.Column106.HeaderText = "Column106"
        Me.Column106.Name = "Column106"
        Me.Column106.ReadOnly = True
        '
        'Column107
        '
        Me.Column107.HeaderText = "Column107"
        Me.Column107.Name = "Column107"
        Me.Column107.ReadOnly = True
        '
        'Column108
        '
        Me.Column108.HeaderText = "Column108"
        Me.Column108.Name = "Column108"
        Me.Column108.ReadOnly = True
        '
        'Column109
        '
        Me.Column109.HeaderText = "Column109"
        Me.Column109.Name = "Column109"
        Me.Column109.ReadOnly = True
        '
        'Column110
        '
        Me.Column110.HeaderText = "Column110"
        Me.Column110.Name = "Column110"
        Me.Column110.ReadOnly = True
        '
        'Column111
        '
        Me.Column111.HeaderText = "Column111"
        Me.Column111.Name = "Column111"
        Me.Column111.ReadOnly = True
        '
        'Column112
        '
        Me.Column112.HeaderText = "Column112"
        Me.Column112.Name = "Column112"
        Me.Column112.ReadOnly = True
        '
        'Column113
        '
        Me.Column113.HeaderText = "Column113"
        Me.Column113.Name = "Column113"
        Me.Column113.ReadOnly = True
        '
        'Column114
        '
        Me.Column114.HeaderText = "Column114"
        Me.Column114.Name = "Column114"
        Me.Column114.ReadOnly = True
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.Panel7.Controls.Add(Me.Label8)
        Me.Panel7.Controls.Add(Me.Label5)
        Me.Panel7.Controls.Add(Me.Label1)
        Me.Panel7.Controls.Add(Me.Label2)
        Me.Panel7.Controls.Add(Me.LabelTTLRowtoSync)
        Me.Panel7.Controls.Add(Me.Label4)
        Me.Panel7.Controls.Add(Me.ProgressBar1)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel7.Location = New System.Drawing.Point(0, 326)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(492, 31)
        Me.Panel7.TabIndex = 24
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(24, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 16)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "Status."
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(492, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(0, 16)
        Me.Label5.TabIndex = 28
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(24, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 16)
        Me.Label1.TabIndex = 24
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(12, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(12, 16)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = " "
        '
        'LabelTTLRowtoSync
        '
        Me.LabelTTLRowtoSync.AutoSize = True
        Me.LabelTTLRowtoSync.Dock = System.Windows.Forms.DockStyle.Left
        Me.LabelTTLRowtoSync.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTTLRowtoSync.ForeColor = System.Drawing.Color.White
        Me.LabelTTLRowtoSync.Location = New System.Drawing.Point(0, 0)
        Me.LabelTTLRowtoSync.Name = "LabelTTLRowtoSync"
        Me.LabelTTLRowtoSync.Size = New System.Drawing.Size(12, 16)
        Me.LabelTTLRowtoSync.TabIndex = 25
        Me.LabelTTLRowtoSync.Text = " "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(0, 16)
        Me.Label4.TabIndex = 27
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 20)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(492, 11)
        Me.ProgressBar1.TabIndex = 11
        '
        'DataGridViewINV
        '
        Me.DataGridViewINV.AllowUserToAddRows = False
        Me.DataGridViewINV.AllowUserToDeleteRows = False
        Me.DataGridViewINV.AllowUserToResizeColumns = False
        Me.DataGridViewINV.AllowUserToResizeRows = False
        Me.DataGridViewINV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewINV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column165, Me.Column166, Me.Column167, Me.Column168, Me.Column169, Me.Column170, Me.Column171, Me.Column172, Me.Column173, Me.Column174, Me.Column175, Me.Column176, Me.Column177, Me.Column178, Me.Column179})
        Me.DataGridViewINV.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewINV.Name = "DataGridViewINV"
        Me.DataGridViewINV.ReadOnly = True
        Me.DataGridViewINV.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewINV.TabIndex = 5
        '
        'Column165
        '
        Me.Column165.HeaderText = "Column165"
        Me.Column165.Name = "Column165"
        Me.Column165.ReadOnly = True
        '
        'Column166
        '
        Me.Column166.HeaderText = "Column166"
        Me.Column166.Name = "Column166"
        Me.Column166.ReadOnly = True
        '
        'Column167
        '
        Me.Column167.HeaderText = "Column167"
        Me.Column167.Name = "Column167"
        Me.Column167.ReadOnly = True
        '
        'Column168
        '
        Me.Column168.HeaderText = "Column168"
        Me.Column168.Name = "Column168"
        Me.Column168.ReadOnly = True
        '
        'Column169
        '
        Me.Column169.HeaderText = "Column169"
        Me.Column169.Name = "Column169"
        Me.Column169.ReadOnly = True
        '
        'Column170
        '
        Me.Column170.HeaderText = "Column170"
        Me.Column170.Name = "Column170"
        Me.Column170.ReadOnly = True
        '
        'Column171
        '
        Me.Column171.HeaderText = "Column171"
        Me.Column171.Name = "Column171"
        Me.Column171.ReadOnly = True
        '
        'Column172
        '
        Me.Column172.HeaderText = "Column172"
        Me.Column172.Name = "Column172"
        Me.Column172.ReadOnly = True
        '
        'Column173
        '
        Me.Column173.HeaderText = "Column173"
        Me.Column173.Name = "Column173"
        Me.Column173.ReadOnly = True
        '
        'Column174
        '
        Me.Column174.HeaderText = "Column174"
        Me.Column174.Name = "Column174"
        Me.Column174.ReadOnly = True
        '
        'Column175
        '
        Me.Column175.HeaderText = "Column175"
        Me.Column175.Name = "Column175"
        Me.Column175.ReadOnly = True
        '
        'Column176
        '
        Me.Column176.HeaderText = "Column176"
        Me.Column176.Name = "Column176"
        Me.Column176.ReadOnly = True
        '
        'Column177
        '
        Me.Column177.HeaderText = "Column177"
        Me.Column177.Name = "Column177"
        Me.Column177.ReadOnly = True
        '
        'Column178
        '
        Me.Column178.HeaderText = "Column178"
        Me.Column178.Name = "Column178"
        Me.Column178.ReadOnly = True
        '
        'Column179
        '
        Me.Column179.HeaderText = "Column179"
        Me.Column179.Name = "Column179"
        Me.Column179.ReadOnly = True
        '
        'DataGridViewEXP
        '
        Me.DataGridViewEXP.AllowUserToAddRows = False
        Me.DataGridViewEXP.AllowUserToDeleteRows = False
        Me.DataGridViewEXP.AllowUserToResizeColumns = False
        Me.DataGridViewEXP.AllowUserToResizeRows = False
        Me.DataGridViewEXP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewEXP.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column59, Me.Column60, Me.Column61, Me.Column62, Me.Column63, Me.Column64, Me.Column65, Me.Column66, Me.Column67, Me.Column68, Me.Column69, Me.Column70, Me.Column71})
        Me.DataGridViewEXP.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewEXP.Name = "DataGridViewEXP"
        Me.DataGridViewEXP.ReadOnly = True
        Me.DataGridViewEXP.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewEXP.TabIndex = 8
        '
        'Column59
        '
        Me.Column59.HeaderText = "Column59"
        Me.Column59.Name = "Column59"
        Me.Column59.ReadOnly = True
        '
        'Column60
        '
        Me.Column60.HeaderText = "Column60"
        Me.Column60.Name = "Column60"
        Me.Column60.ReadOnly = True
        '
        'Column61
        '
        Me.Column61.HeaderText = "Column61"
        Me.Column61.Name = "Column61"
        Me.Column61.ReadOnly = True
        '
        'Column62
        '
        Me.Column62.HeaderText = "Column62"
        Me.Column62.Name = "Column62"
        Me.Column62.ReadOnly = True
        '
        'Column63
        '
        Me.Column63.HeaderText = "Column63"
        Me.Column63.Name = "Column63"
        Me.Column63.ReadOnly = True
        '
        'Column64
        '
        Me.Column64.HeaderText = "Column64"
        Me.Column64.Name = "Column64"
        Me.Column64.ReadOnly = True
        '
        'Column65
        '
        Me.Column65.HeaderText = "Column65"
        Me.Column65.Name = "Column65"
        Me.Column65.ReadOnly = True
        '
        'Column66
        '
        Me.Column66.HeaderText = "Column66"
        Me.Column66.Name = "Column66"
        Me.Column66.ReadOnly = True
        '
        'Column67
        '
        Me.Column67.HeaderText = "Column67"
        Me.Column67.Name = "Column67"
        Me.Column67.ReadOnly = True
        '
        'Column68
        '
        Me.Column68.HeaderText = "Column68"
        Me.Column68.Name = "Column68"
        Me.Column68.ReadOnly = True
        '
        'Column69
        '
        Me.Column69.HeaderText = "Column69"
        Me.Column69.Name = "Column69"
        Me.Column69.ReadOnly = True
        '
        'Column70
        '
        Me.Column70.HeaderText = "Column70"
        Me.Column70.Name = "Column70"
        Me.Column70.ReadOnly = True
        '
        'Column71
        '
        Me.Column71.HeaderText = "Column71"
        Me.Column71.Name = "Column71"
        Me.Column71.ReadOnly = True
        '
        'DataGridViewTRAN
        '
        Me.DataGridViewTRAN.AllowUserToAddRows = False
        Me.DataGridViewTRAN.AllowUserToDeleteRows = False
        Me.DataGridViewTRAN.AllowUserToResizeColumns = False
        Me.DataGridViewTRAN.AllowUserToResizeRows = False
        Me.DataGridViewTRAN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewTRAN.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewTRAN.Name = "DataGridViewTRAN"
        Me.DataGridViewTRAN.ReadOnly = True
        Me.DataGridViewTRAN.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewTRAN.TabIndex = 2
        '
        'ButtonSYNCINVENTORY
        '
        Me.ButtonSYNCINVENTORY.BackColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.ButtonSYNCINVENTORY.FlatAppearance.BorderSize = 0
        Me.ButtonSYNCINVENTORY.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSYNCINVENTORY.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSYNCINVENTORY.ForeColor = System.Drawing.Color.White
        Me.ButtonSYNCINVENTORY.Location = New System.Drawing.Point(506, 186)
        Me.ButtonSYNCINVENTORY.Name = "ButtonSYNCINVENTORY"
        Me.ButtonSYNCINVENTORY.Size = New System.Drawing.Size(228, 41)
        Me.ButtonSYNCINVENTORY.TabIndex = 42
        Me.ButtonSYNCINVENTORY.Text = "SYNC INVENTORY"
        Me.ButtonSYNCINVENTORY.UseVisualStyleBackColor = False
        '
        'LabelZREADINVITEM
        '
        Me.LabelZREADINVITEM.AutoSize = True
        Me.LabelZREADINVITEM.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelZREADINVITEM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelZREADINVITEM.Location = New System.Drawing.Point(475, 56)
        Me.LabelZREADINVITEM.Name = "LabelZREADINVITEM"
        Me.LabelZREADINVITEM.Size = New System.Drawing.Size(14, 14)
        Me.LabelZREADINVITEM.TabIndex = 56
        Me.LabelZREADINVITEM.Text = "0"
        '
        'LabelZREADINVTIME
        '
        Me.LabelZREADINVTIME.AutoSize = True
        Me.LabelZREADINVTIME.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelZREADINVTIME.Location = New System.Drawing.Point(175, 56)
        Me.LabelZREADINVTIME.Name = "LabelZREADINVTIME"
        Me.LabelZREADINVTIME.Size = New System.Drawing.Size(165, 14)
        Me.LabelZREADINVTIME.TabIndex = 55
        Me.LabelZREADINVTIME.Text = "Estimating Time. Please Wait"
        '
        'LabelZREADINV
        '
        Me.LabelZREADINV.AutoSize = True
        Me.LabelZREADINV.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelZREADINV.Location = New System.Drawing.Point(3, 56)
        Me.LabelZREADINV.Name = "LabelZREADINV"
        Me.LabelZREADINV.Size = New System.Drawing.Size(95, 14)
        Me.LabelZREADINV.TabIndex = 54
        Me.LabelZREADINV.Text = "Zread Inventory"
        '
        'LabelErrorItem
        '
        Me.LabelErrorItem.AutoSize = True
        Me.LabelErrorItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelErrorItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelErrorItem.Location = New System.Drawing.Point(475, 196)
        Me.LabelErrorItem.Name = "LabelErrorItem"
        Me.LabelErrorItem.Size = New System.Drawing.Size(14, 14)
        Me.LabelErrorItem.TabIndex = 53
        Me.LabelErrorItem.Text = "0"
        '
        'LabelErrorTime
        '
        Me.LabelErrorTime.AutoSize = True
        Me.LabelErrorTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelErrorTime.Location = New System.Drawing.Point(175, 196)
        Me.LabelErrorTime.Name = "LabelErrorTime"
        Me.LabelErrorTime.Size = New System.Drawing.Size(165, 14)
        Me.LabelErrorTime.TabIndex = 52
        Me.LabelErrorTime.Text = "Estimating Time. Please Wait"
        '
        'LabelError
        '
        Me.LabelError.AutoSize = True
        Me.LabelError.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelError.Location = New System.Drawing.Point(3, 196)
        Me.LabelError.Name = "LabelError"
        Me.LabelError.Size = New System.Drawing.Size(38, 14)
        Me.LabelError.TabIndex = 51
        Me.LabelError.Text = "Errors"
        '
        'LabelCouponItem
        '
        Me.LabelCouponItem.AutoSize = True
        Me.LabelCouponItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelCouponItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCouponItem.Location = New System.Drawing.Point(475, 182)
        Me.LabelCouponItem.Name = "LabelCouponItem"
        Me.LabelCouponItem.Size = New System.Drawing.Size(14, 14)
        Me.LabelCouponItem.TabIndex = 50
        Me.LabelCouponItem.Text = "0"
        '
        'LabelCouponTime
        '
        Me.LabelCouponTime.AutoSize = True
        Me.LabelCouponTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCouponTime.Location = New System.Drawing.Point(175, 182)
        Me.LabelCouponTime.Name = "LabelCouponTime"
        Me.LabelCouponTime.Size = New System.Drawing.Size(165, 14)
        Me.LabelCouponTime.TabIndex = 49
        Me.LabelCouponTime.Text = "Estimating Time. Please Wait"
        '
        'LabelCoupon
        '
        Me.LabelCoupon.AutoSize = True
        Me.LabelCoupon.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCoupon.Location = New System.Drawing.Point(3, 182)
        Me.LabelCoupon.Name = "LabelCoupon"
        Me.LabelCoupon.Size = New System.Drawing.Size(54, 14)
        Me.LabelCoupon.TabIndex = 48
        Me.LabelCoupon.Text = "Coupons"
        '
        'LabelPRICEREQItem
        '
        Me.LabelPRICEREQItem.AutoSize = True
        Me.LabelPRICEREQItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelPRICEREQItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPRICEREQItem.Location = New System.Drawing.Point(475, 168)
        Me.LabelPRICEREQItem.Name = "LabelPRICEREQItem"
        Me.LabelPRICEREQItem.Size = New System.Drawing.Size(14, 14)
        Me.LabelPRICEREQItem.TabIndex = 47
        Me.LabelPRICEREQItem.Text = "0"
        '
        'LabelPRICEREQTime
        '
        Me.LabelPRICEREQTime.AutoSize = True
        Me.LabelPRICEREQTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPRICEREQTime.Location = New System.Drawing.Point(175, 168)
        Me.LabelPRICEREQTime.Name = "LabelPRICEREQTime"
        Me.LabelPRICEREQTime.Size = New System.Drawing.Size(165, 14)
        Me.LabelPRICEREQTime.TabIndex = 46
        Me.LabelPRICEREQTime.Text = "Estimating Time. Please Wait"
        '
        'LabelPRICEREQ
        '
        Me.LabelPRICEREQ.AutoSize = True
        Me.LabelPRICEREQ.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPRICEREQ.Location = New System.Drawing.Point(3, 168)
        Me.LabelPRICEREQ.Name = "LabelPRICEREQ"
        Me.LabelPRICEREQ.Size = New System.Drawing.Size(127, 14)
        Me.LabelPRICEREQ.TabIndex = 45
        Me.LabelPRICEREQ.Text = "Price Request Change"
        '
        'LabelDEPOSITItem
        '
        Me.LabelDEPOSITItem.AutoSize = True
        Me.LabelDEPOSITItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelDEPOSITItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDEPOSITItem.Location = New System.Drawing.Point(475, 154)
        Me.LabelDEPOSITItem.Name = "LabelDEPOSITItem"
        Me.LabelDEPOSITItem.Size = New System.Drawing.Size(14, 14)
        Me.LabelDEPOSITItem.TabIndex = 44
        Me.LabelDEPOSITItem.Text = "0"
        '
        'LabelDEPOSITTime
        '
        Me.LabelDEPOSITTime.AutoSize = True
        Me.LabelDEPOSITTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDEPOSITTime.Location = New System.Drawing.Point(175, 154)
        Me.LabelDEPOSITTime.Name = "LabelDEPOSITTime"
        Me.LabelDEPOSITTime.Size = New System.Drawing.Size(165, 14)
        Me.LabelDEPOSITTime.TabIndex = 43
        Me.LabelDEPOSITTime.Text = "Estimating Time. Please Wait"
        '
        'LabelDEPOSIT
        '
        Me.LabelDEPOSIT.AutoSize = True
        Me.LabelDEPOSIT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDEPOSIT.Location = New System.Drawing.Point(3, 154)
        Me.LabelDEPOSIT.Name = "LabelDEPOSIT"
        Me.LabelDEPOSIT.Size = New System.Drawing.Size(48, 14)
        Me.LabelDEPOSIT.TabIndex = 42
        Me.LabelDEPOSIT.Text = "Deposit"
        '
        'LabelMODETItem
        '
        Me.LabelMODETItem.AutoSize = True
        Me.LabelMODETItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelMODETItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelMODETItem.Location = New System.Drawing.Point(475, 140)
        Me.LabelMODETItem.Name = "LabelMODETItem"
        Me.LabelMODETItem.Size = New System.Drawing.Size(14, 14)
        Me.LabelMODETItem.TabIndex = 41
        Me.LabelMODETItem.Text = "0"
        '
        'LabelMODETTime
        '
        Me.LabelMODETTime.AutoSize = True
        Me.LabelMODETTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelMODETTime.Location = New System.Drawing.Point(175, 140)
        Me.LabelMODETTime.Name = "LabelMODETTime"
        Me.LabelMODETTime.Size = New System.Drawing.Size(165, 14)
        Me.LabelMODETTime.TabIndex = 40
        Me.LabelMODETTime.Text = "Estimating Time. Please Wait"
        '
        'LabelMODET
        '
        Me.LabelMODET.AutoSize = True
        Me.LabelMODET.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelMODET.Location = New System.Drawing.Point(3, 140)
        Me.LabelMODET.Name = "LabelMODET"
        Me.LabelMODET.Size = New System.Drawing.Size(119, 14)
        Me.LabelMODET.TabIndex = 39
        Me.LabelMODET.Text = "Mode of Transaction"
        '
        'LabelCPRODItem
        '
        Me.LabelCPRODItem.AutoSize = True
        Me.LabelCPRODItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelCPRODItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCPRODItem.Location = New System.Drawing.Point(475, 126)
        Me.LabelCPRODItem.Name = "LabelCPRODItem"
        Me.LabelCPRODItem.Size = New System.Drawing.Size(14, 14)
        Me.LabelCPRODItem.TabIndex = 38
        Me.LabelCPRODItem.Text = "0"
        '
        'LabelCPRODTime
        '
        Me.LabelCPRODTime.AutoSize = True
        Me.LabelCPRODTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCPRODTime.Location = New System.Drawing.Point(175, 126)
        Me.LabelCPRODTime.Name = "LabelCPRODTime"
        Me.LabelCPRODTime.Size = New System.Drawing.Size(165, 14)
        Me.LabelCPRODTime.TabIndex = 37
        Me.LabelCPRODTime.Text = "Estimating Time. Please Wait"
        '
        'LabelCPROD
        '
        Me.LabelCPROD.AutoSize = True
        Me.LabelCPROD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCPROD.Location = New System.Drawing.Point(3, 126)
        Me.LabelCPROD.Name = "LabelCPROD"
        Me.LabelCPROD.Size = New System.Drawing.Size(100, 14)
        Me.LabelCPROD.TabIndex = 36
        Me.LabelCPROD.Text = "Custom Products"
        '
        'LabelRETItem
        '
        Me.LabelRETItem.AutoSize = True
        Me.LabelRETItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelRETItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelRETItem.Location = New System.Drawing.Point(475, 112)
        Me.LabelRETItem.Name = "LabelRETItem"
        Me.LabelRETItem.Size = New System.Drawing.Size(14, 14)
        Me.LabelRETItem.TabIndex = 35
        Me.LabelRETItem.Text = "0"
        '
        'LabelRETTime
        '
        Me.LabelRETTime.AutoSize = True
        Me.LabelRETTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelRETTime.Location = New System.Drawing.Point(175, 112)
        Me.LabelRETTime.Name = "LabelRETTime"
        Me.LabelRETTime.Size = New System.Drawing.Size(165, 14)
        Me.LabelRETTime.TabIndex = 34
        Me.LabelRETTime.Text = "Estimating Time. Please Wait"
        '
        'LabelRET
        '
        Me.LabelRET.AutoSize = True
        Me.LabelRET.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelRET.Location = New System.Drawing.Point(3, 112)
        Me.LabelRET.Name = "LabelRET"
        Me.LabelRET.Size = New System.Drawing.Size(49, 14)
        Me.LabelRET.TabIndex = 33
        Me.LabelRET.Text = "Returns"
        '
        'LabelSYS1Item
        '
        Me.LabelSYS1Item.AutoSize = True
        Me.LabelSYS1Item.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelSYS1Item.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSYS1Item.Location = New System.Drawing.Point(475, 0)
        Me.LabelSYS1Item.Name = "LabelSYS1Item"
        Me.LabelSYS1Item.Size = New System.Drawing.Size(14, 14)
        Me.LabelSYS1Item.TabIndex = 29
        Me.LabelSYS1Item.Text = "0"
        '
        'LabelACCItem
        '
        Me.LabelACCItem.AutoSize = True
        Me.LabelACCItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelACCItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelACCItem.Location = New System.Drawing.Point(475, 98)
        Me.LabelACCItem.Name = "LabelACCItem"
        Me.LabelACCItem.Size = New System.Drawing.Size(14, 14)
        Me.LabelACCItem.TabIndex = 27
        Me.LabelACCItem.Text = "0"
        '
        'LabelEXPDItem
        '
        Me.LabelEXPDItem.AutoSize = True
        Me.LabelEXPDItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelEXPDItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelEXPDItem.Location = New System.Drawing.Point(475, 84)
        Me.LabelEXPDItem.Name = "LabelEXPDItem"
        Me.LabelEXPDItem.Size = New System.Drawing.Size(14, 14)
        Me.LabelEXPDItem.TabIndex = 26
        Me.LabelEXPDItem.Text = "0"
        '
        'LabelEXPItem
        '
        Me.LabelEXPItem.AutoSize = True
        Me.LabelEXPItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelEXPItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelEXPItem.Location = New System.Drawing.Point(475, 70)
        Me.LabelEXPItem.Name = "LabelEXPItem"
        Me.LabelEXPItem.Size = New System.Drawing.Size(14, 14)
        Me.LabelEXPItem.TabIndex = 25
        Me.LabelEXPItem.Text = "0"
        '
        'LabelINVItem
        '
        Me.LabelINVItem.AutoSize = True
        Me.LabelINVItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelINVItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelINVItem.Location = New System.Drawing.Point(475, 42)
        Me.LabelINVItem.Name = "LabelINVItem"
        Me.LabelINVItem.Size = New System.Drawing.Size(14, 14)
        Me.LabelINVItem.TabIndex = 24
        Me.LabelINVItem.Text = "0"
        '
        'LabelDTransactDItem
        '
        Me.LabelDTransactDItem.AutoSize = True
        Me.LabelDTransactDItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelDTransactDItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDTransactDItem.Location = New System.Drawing.Point(475, 28)
        Me.LabelDTransactDItem.Name = "LabelDTransactDItem"
        Me.LabelDTransactDItem.Size = New System.Drawing.Size(14, 14)
        Me.LabelDTransactDItem.TabIndex = 23
        Me.LabelDTransactDItem.Text = "0"
        '
        'LabelDTransacItem
        '
        Me.LabelDTransacItem.AutoSize = True
        Me.LabelDTransacItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelDTransacItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDTransacItem.Location = New System.Drawing.Point(475, 14)
        Me.LabelDTransacItem.Name = "LabelDTransacItem"
        Me.LabelDTransacItem.Size = New System.Drawing.Size(14, 14)
        Me.LabelDTransacItem.TabIndex = 22
        Me.LabelDTransacItem.Text = "0"
        '
        'LabelSYS1Time
        '
        Me.LabelSYS1Time.AutoSize = True
        Me.LabelSYS1Time.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSYS1Time.Location = New System.Drawing.Point(175, 0)
        Me.LabelSYS1Time.Name = "LabelSYS1Time"
        Me.LabelSYS1Time.Size = New System.Drawing.Size(165, 14)
        Me.LabelSYS1Time.TabIndex = 18
        Me.LabelSYS1Time.Text = "Estimating Time. Please Wait"
        '
        'LabelSYS1
        '
        Me.LabelSYS1.AutoSize = True
        Me.LabelSYS1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSYS1.Location = New System.Drawing.Point(3, 0)
        Me.LabelSYS1.Name = "LabelSYS1"
        Me.LabelSYS1.Size = New System.Drawing.Size(76, 14)
        Me.LabelSYS1.TabIndex = 14
        Me.LabelSYS1.Text = "System Logs"
        '
        'LabelACCTime
        '
        Me.LabelACCTime.AutoSize = True
        Me.LabelACCTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelACCTime.Location = New System.Drawing.Point(175, 98)
        Me.LabelACCTime.Name = "LabelACCTime"
        Me.LabelACCTime.Size = New System.Drawing.Size(165, 14)
        Me.LabelACCTime.TabIndex = 12
        Me.LabelACCTime.Text = "Estimating Time. Please Wait"
        '
        'LabelEXPDTime
        '
        Me.LabelEXPDTime.AutoSize = True
        Me.LabelEXPDTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelEXPDTime.Location = New System.Drawing.Point(175, 84)
        Me.LabelEXPDTime.Name = "LabelEXPDTime"
        Me.LabelEXPDTime.Size = New System.Drawing.Size(165, 14)
        Me.LabelEXPDTime.TabIndex = 11
        Me.LabelEXPDTime.Text = "Estimating Time. Please Wait"
        '
        'LabelEXPTime
        '
        Me.LabelEXPTime.AutoSize = True
        Me.LabelEXPTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelEXPTime.Location = New System.Drawing.Point(175, 70)
        Me.LabelEXPTime.Name = "LabelEXPTime"
        Me.LabelEXPTime.Size = New System.Drawing.Size(165, 14)
        Me.LabelEXPTime.TabIndex = 10
        Me.LabelEXPTime.Text = "Estimating Time. Please Wait"
        '
        'LabelINVTime
        '
        Me.LabelINVTime.AutoSize = True
        Me.LabelINVTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelINVTime.Location = New System.Drawing.Point(175, 42)
        Me.LabelINVTime.Name = "LabelINVTime"
        Me.LabelINVTime.Size = New System.Drawing.Size(165, 14)
        Me.LabelINVTime.TabIndex = 9
        Me.LabelINVTime.Text = "Estimating Time. Please Wait"
        '
        'LabelDTransactDTime
        '
        Me.LabelDTransactDTime.AutoSize = True
        Me.LabelDTransactDTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDTransactDTime.Location = New System.Drawing.Point(175, 28)
        Me.LabelDTransactDTime.Name = "LabelDTransactDTime"
        Me.LabelDTransactDTime.Size = New System.Drawing.Size(165, 14)
        Me.LabelDTransactDTime.TabIndex = 8
        Me.LabelDTransactDTime.Text = "Estimating Time. Please Wait"
        '
        'LabelDTransacTime
        '
        Me.LabelDTransacTime.AutoSize = True
        Me.LabelDTransacTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDTransacTime.Location = New System.Drawing.Point(175, 14)
        Me.LabelDTransacTime.Name = "LabelDTransacTime"
        Me.LabelDTransacTime.Size = New System.Drawing.Size(165, 14)
        Me.LabelDTransacTime.TabIndex = 7
        Me.LabelDTransacTime.Text = "Estimating Time. Please Wait"
        '
        'LabelACC
        '
        Me.LabelACC.AutoSize = True
        Me.LabelACC.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelACC.Location = New System.Drawing.Point(3, 98)
        Me.LabelACC.Name = "LabelACC"
        Me.LabelACC.Size = New System.Drawing.Size(58, 14)
        Me.LabelACC.TabIndex = 5
        Me.LabelACC.Text = "Accounts"
        '
        'LabelEXPD
        '
        Me.LabelEXPD.AutoSize = True
        Me.LabelEXPD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelEXPD.Location = New System.Drawing.Point(3, 84)
        Me.LabelEXPD.Name = "LabelEXPD"
        Me.LabelEXPD.Size = New System.Drawing.Size(92, 14)
        Me.LabelEXPD.TabIndex = 4
        Me.LabelEXPD.Text = "Expense Details"
        '
        'LabelEXP
        '
        Me.LabelEXP.AutoSize = True
        Me.LabelEXP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelEXP.Location = New System.Drawing.Point(3, 70)
        Me.LabelEXP.Name = "LabelEXP"
        Me.LabelEXP.Size = New System.Drawing.Size(75, 14)
        Me.LabelEXP.TabIndex = 3
        Me.LabelEXP.Text = "Expense List"
        '
        'LabelINV
        '
        Me.LabelINV.AutoSize = True
        Me.LabelINV.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelINV.Location = New System.Drawing.Point(3, 42)
        Me.LabelINV.Name = "LabelINV"
        Me.LabelINV.Size = New System.Drawing.Size(60, 14)
        Me.LabelINV.TabIndex = 2
        Me.LabelINV.Text = "Inventory"
        '
        'LabelDTransactD
        '
        Me.LabelDTransactD.AutoSize = True
        Me.LabelDTransactD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDTransactD.Location = New System.Drawing.Point(3, 28)
        Me.LabelDTransactD.Name = "LabelDTransactD"
        Me.LabelDTransactD.Size = New System.Drawing.Size(137, 14)
        Me.LabelDTransactD.TabIndex = 1
        Me.LabelDTransactD.Text = "Daily Transaction Details"
        '
        'LabelDTransac
        '
        Me.LabelDTransac.AutoSize = True
        Me.LabelDTransac.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDTransac.Location = New System.Drawing.Point(3, 14)
        Me.LabelDTransac.Name = "LabelDTransac"
        Me.LabelDTransac.Size = New System.Drawing.Size(98, 14)
        Me.LabelDTransac.TabIndex = 0
        Me.LabelDTransac.Text = "Daily Transaction"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DataGridViewZREADINVENTORY)
        Me.GroupBox1.Controls.Add(Me.DatagridviewSenior)
        Me.GroupBox1.Controls.Add(Me.DataGridViewERRORS)
        Me.GroupBox1.Controls.Add(Me.DataGridViewCoupons)
        Me.GroupBox1.Controls.Add(Me.LabelTime)
        Me.GroupBox1.Controls.Add(Me.LabelRowtoSync)
        Me.GroupBox1.Controls.Add(Me.DataGridViewINV)
        Me.GroupBox1.Controls.Add(Me.DataGridView2)
        Me.GroupBox1.Controls.Add(Me.DataGridViewPriceChangeRequest)
        Me.GroupBox1.Controls.Add(Me.DataGridViewDepositSlip)
        Me.GroupBox1.Controls.Add(Me.DataGridViewMODEOFTRANSACTION)
        Me.GroupBox1.Controls.Add(Me.DataGridViewRetrefdetails)
        Me.GroupBox1.Controls.Add(Me.DataGridViewEXP)
        Me.GroupBox1.Controls.Add(Me.DataGridViewTRAN)
        Me.GroupBox1.Controls.Add(Me.DataGridViewSYSLOG2)
        Me.GroupBox1.Controls.Add(Me.DataGridViewLocusers)
        Me.GroupBox1.Controls.Add(Me.DataGridViewSYSLOG1)
        Me.GroupBox1.Controls.Add(Me.DataGridViewTRANDET)
        Me.GroupBox1.Controls.Add(Me.DataGridView1)
        Me.GroupBox1.Controls.Add(Me.DataGridViewCUSTOMPRODUCTS)
        Me.GroupBox1.Controls.Add(Me.DataGridViewSYSLOG4)
        Me.GroupBox1.Controls.Add(Me.DataGridViewEXPDET)
        Me.GroupBox1.Controls.Add(Me.DataGridViewSYSLOG3)
        Me.GroupBox1.Location = New System.Drawing.Point(506, 57)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(228, 85)
        Me.GroupBox1.TabIndex = 28
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "GroupBox1"
        Me.GroupBox1.Visible = False
        '
        'DataGridViewZREADINVENTORY
        '
        Me.DataGridViewZREADINVENTORY.AllowUserToAddRows = False
        Me.DataGridViewZREADINVENTORY.AllowUserToDeleteRows = False
        Me.DataGridViewZREADINVENTORY.AllowUserToResizeColumns = False
        Me.DataGridViewZREADINVENTORY.AllowUserToResizeRows = False
        Me.DataGridViewZREADINVENTORY.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewZREADINVENTORY.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column116, Me.Column117, Me.Column118, Me.Column119, Me.Column120, Me.Column121, Me.Column122, Me.Column123, Me.Column124, Me.Column125, Me.Column126, Me.Column127, Me.Column128, Me.Column129, Me.Column130, Me.Column131, Me.Column132})
        Me.DataGridViewZREADINVENTORY.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewZREADINVENTORY.Name = "DataGridViewZREADINVENTORY"
        Me.DataGridViewZREADINVENTORY.ReadOnly = True
        Me.DataGridViewZREADINVENTORY.Size = New System.Drawing.Size(188, 51)
        Me.DataGridViewZREADINVENTORY.TabIndex = 44
        '
        'Column116
        '
        Me.Column116.HeaderText = "Column116"
        Me.Column116.Name = "Column116"
        Me.Column116.ReadOnly = True
        '
        'Column117
        '
        Me.Column117.HeaderText = "Column117"
        Me.Column117.Name = "Column117"
        Me.Column117.ReadOnly = True
        '
        'Column118
        '
        Me.Column118.HeaderText = "Column118"
        Me.Column118.Name = "Column118"
        Me.Column118.ReadOnly = True
        '
        'Column119
        '
        Me.Column119.HeaderText = "Column119"
        Me.Column119.Name = "Column119"
        Me.Column119.ReadOnly = True
        '
        'Column120
        '
        Me.Column120.HeaderText = "Column120"
        Me.Column120.Name = "Column120"
        Me.Column120.ReadOnly = True
        '
        'Column121
        '
        Me.Column121.HeaderText = "Column121"
        Me.Column121.Name = "Column121"
        Me.Column121.ReadOnly = True
        '
        'Column122
        '
        Me.Column122.HeaderText = "Column122"
        Me.Column122.Name = "Column122"
        Me.Column122.ReadOnly = True
        '
        'Column123
        '
        Me.Column123.HeaderText = "Column123"
        Me.Column123.Name = "Column123"
        Me.Column123.ReadOnly = True
        '
        'Column124
        '
        Me.Column124.HeaderText = "Column124"
        Me.Column124.Name = "Column124"
        Me.Column124.ReadOnly = True
        '
        'Column125
        '
        Me.Column125.HeaderText = "Column125"
        Me.Column125.Name = "Column125"
        Me.Column125.ReadOnly = True
        '
        'Column126
        '
        Me.Column126.HeaderText = "Column126"
        Me.Column126.Name = "Column126"
        Me.Column126.ReadOnly = True
        '
        'Column127
        '
        Me.Column127.HeaderText = "Column127"
        Me.Column127.Name = "Column127"
        Me.Column127.ReadOnly = True
        '
        'Column128
        '
        Me.Column128.HeaderText = "Column128"
        Me.Column128.Name = "Column128"
        Me.Column128.ReadOnly = True
        '
        'Column129
        '
        Me.Column129.HeaderText = "Column129"
        Me.Column129.Name = "Column129"
        Me.Column129.ReadOnly = True
        '
        'Column130
        '
        Me.Column130.HeaderText = "Column130"
        Me.Column130.Name = "Column130"
        Me.Column130.ReadOnly = True
        '
        'Column131
        '
        Me.Column131.HeaderText = "Column131"
        Me.Column131.Name = "Column131"
        Me.Column131.ReadOnly = True
        '
        'Column132
        '
        Me.Column132.HeaderText = "Column132"
        Me.Column132.Name = "Column132"
        Me.Column132.ReadOnly = True
        '
        'DatagridviewSenior
        '
        Me.DatagridviewSenior.AllowUserToAddRows = False
        Me.DatagridviewSenior.AllowUserToDeleteRows = False
        Me.DatagridviewSenior.AllowUserToResizeColumns = False
        Me.DatagridviewSenior.AllowUserToResizeRows = False
        Me.DatagridviewSenior.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DatagridviewSenior.Location = New System.Drawing.Point(6, 19)
        Me.DatagridviewSenior.Name = "DatagridviewSenior"
        Me.DatagridviewSenior.RowHeadersVisible = False
        Me.DatagridviewSenior.Size = New System.Drawing.Size(230, 80)
        Me.DatagridviewSenior.TabIndex = 63
        '
        'DataGridViewERRORS
        '
        Me.DataGridViewERRORS.AllowUserToAddRows = False
        Me.DataGridViewERRORS.AllowUserToDeleteRows = False
        Me.DataGridViewERRORS.AllowUserToResizeColumns = False
        Me.DataGridViewERRORS.AllowUserToResizeRows = False
        Me.DataGridViewERRORS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewERRORS.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewERRORS.Name = "DataGridViewERRORS"
        Me.DataGridViewERRORS.ReadOnly = True
        Me.DataGridViewERRORS.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewERRORS.TabIndex = 43
        '
        'DataGridViewCoupons
        '
        Me.DataGridViewCoupons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewCoupons.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewCoupons.Name = "DataGridViewCoupons"
        Me.DataGridViewCoupons.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewCoupons.TabIndex = 42
        '
        'LabelTime
        '
        Me.LabelTime.AutoSize = True
        Me.LabelTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTime.Location = New System.Drawing.Point(242, 19)
        Me.LabelTime.Name = "LabelTime"
        Me.LabelTime.Size = New System.Drawing.Size(15, 16)
        Me.LabelTime.TabIndex = 27
        Me.LabelTime.Text = "0"
        '
        'LabelRowtoSync
        '
        Me.LabelRowtoSync.AutoSize = True
        Me.LabelRowtoSync.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelRowtoSync.Location = New System.Drawing.Point(242, 36)
        Me.LabelRowtoSync.Name = "LabelRowtoSync"
        Me.LabelRowtoSync.Size = New System.Drawing.Size(15, 16)
        Me.LabelRowtoSync.TabIndex = 31
        Me.LabelRowtoSync.Text = "0"
        '
        'DataGridViewPriceChangeRequest
        '
        Me.DataGridViewPriceChangeRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewPriceChangeRequest.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewPriceChangeRequest.Name = "DataGridViewPriceChangeRequest"
        Me.DataGridViewPriceChangeRequest.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewPriceChangeRequest.TabIndex = 42
        '
        'DataGridViewDepositSlip
        '
        Me.DataGridViewDepositSlip.AllowUserToAddRows = False
        Me.DataGridViewDepositSlip.AllowUserToDeleteRows = False
        Me.DataGridViewDepositSlip.AllowUserToResizeColumns = False
        Me.DataGridViewDepositSlip.AllowUserToResizeRows = False
        Me.DataGridViewDepositSlip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewDepositSlip.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column31, Me.Column32, Me.Column33, Me.Column34, Me.Column35, Me.Column36, Me.Column37, Me.Column38, Me.Column39, Me.Column40, Me.Column41})
        Me.DataGridViewDepositSlip.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewDepositSlip.Name = "DataGridViewDepositSlip"
        Me.DataGridViewDepositSlip.ReadOnly = True
        Me.DataGridViewDepositSlip.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewDepositSlip.TabIndex = 45
        '
        'Column31
        '
        Me.Column31.HeaderText = "Column31"
        Me.Column31.Name = "Column31"
        Me.Column31.ReadOnly = True
        '
        'Column32
        '
        Me.Column32.HeaderText = "Column32"
        Me.Column32.Name = "Column32"
        Me.Column32.ReadOnly = True
        '
        'Column33
        '
        Me.Column33.HeaderText = "Column33"
        Me.Column33.Name = "Column33"
        Me.Column33.ReadOnly = True
        '
        'Column34
        '
        Me.Column34.HeaderText = "Column34"
        Me.Column34.Name = "Column34"
        Me.Column34.ReadOnly = True
        '
        'Column35
        '
        Me.Column35.HeaderText = "Column35"
        Me.Column35.Name = "Column35"
        Me.Column35.ReadOnly = True
        '
        'Column36
        '
        Me.Column36.HeaderText = "Column36"
        Me.Column36.Name = "Column36"
        Me.Column36.ReadOnly = True
        '
        'Column37
        '
        Me.Column37.HeaderText = "Column37"
        Me.Column37.Name = "Column37"
        Me.Column37.ReadOnly = True
        '
        'Column38
        '
        Me.Column38.HeaderText = "Column38"
        Me.Column38.Name = "Column38"
        Me.Column38.ReadOnly = True
        '
        'Column39
        '
        Me.Column39.HeaderText = "Column39"
        Me.Column39.Name = "Column39"
        Me.Column39.ReadOnly = True
        '
        'Column40
        '
        Me.Column40.HeaderText = "Column40"
        Me.Column40.Name = "Column40"
        Me.Column40.ReadOnly = True
        '
        'Column41
        '
        Me.Column41.HeaderText = "Column41"
        Me.Column41.Name = "Column41"
        Me.Column41.ReadOnly = True
        '
        'DataGridViewMODEOFTRANSACTION
        '
        Me.DataGridViewMODEOFTRANSACTION.AllowUserToAddRows = False
        Me.DataGridViewMODEOFTRANSACTION.AllowUserToDeleteRows = False
        Me.DataGridViewMODEOFTRANSACTION.AllowUserToResizeColumns = False
        Me.DataGridViewMODEOFTRANSACTION.AllowUserToResizeRows = False
        Me.DataGridViewMODEOFTRANSACTION.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewMODEOFTRANSACTION.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column88, Me.Column89, Me.Column90, Me.Column91, Me.Column92, Me.Column93, Me.Column94, Me.Column95, Me.Column96, Me.Column97, Me.Column98})
        Me.DataGridViewMODEOFTRANSACTION.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewMODEOFTRANSACTION.Name = "DataGridViewMODEOFTRANSACTION"
        Me.DataGridViewMODEOFTRANSACTION.ReadOnly = True
        Me.DataGridViewMODEOFTRANSACTION.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewMODEOFTRANSACTION.TabIndex = 44
        '
        'Column88
        '
        Me.Column88.HeaderText = "Column88"
        Me.Column88.Name = "Column88"
        Me.Column88.ReadOnly = True
        '
        'Column89
        '
        Me.Column89.HeaderText = "Column89"
        Me.Column89.Name = "Column89"
        Me.Column89.ReadOnly = True
        '
        'Column90
        '
        Me.Column90.HeaderText = "Column90"
        Me.Column90.Name = "Column90"
        Me.Column90.ReadOnly = True
        '
        'Column91
        '
        Me.Column91.HeaderText = "Column91"
        Me.Column91.Name = "Column91"
        Me.Column91.ReadOnly = True
        '
        'Column92
        '
        Me.Column92.HeaderText = "Column92"
        Me.Column92.Name = "Column92"
        Me.Column92.ReadOnly = True
        '
        'Column93
        '
        Me.Column93.HeaderText = "Column93"
        Me.Column93.Name = "Column93"
        Me.Column93.ReadOnly = True
        '
        'Column94
        '
        Me.Column94.HeaderText = "Column94"
        Me.Column94.Name = "Column94"
        Me.Column94.ReadOnly = True
        '
        'Column95
        '
        Me.Column95.HeaderText = "Column95"
        Me.Column95.Name = "Column95"
        Me.Column95.ReadOnly = True
        '
        'Column96
        '
        Me.Column96.HeaderText = "Column96"
        Me.Column96.Name = "Column96"
        Me.Column96.ReadOnly = True
        '
        'Column97
        '
        Me.Column97.HeaderText = "Column97"
        Me.Column97.Name = "Column97"
        Me.Column97.ReadOnly = True
        '
        'Column98
        '
        Me.Column98.HeaderText = "Column98"
        Me.Column98.Name = "Column98"
        Me.Column98.ReadOnly = True
        '
        'DataGridViewRetrefdetails
        '
        Me.DataGridViewRetrefdetails.AllowUserToAddRows = False
        Me.DataGridViewRetrefdetails.AllowUserToDeleteRows = False
        Me.DataGridViewRetrefdetails.AllowUserToResizeColumns = False
        Me.DataGridViewRetrefdetails.AllowUserToResizeRows = False
        Me.DataGridViewRetrefdetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewRetrefdetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column155, Me.Column156, Me.Column157, Me.Column158, Me.Column159, Me.Column160, Me.Column161, Me.Column162, Me.Column163, Me.Column164})
        Me.DataGridViewRetrefdetails.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewRetrefdetails.Name = "DataGridViewRetrefdetails"
        Me.DataGridViewRetrefdetails.ReadOnly = True
        Me.DataGridViewRetrefdetails.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewRetrefdetails.TabIndex = 42
        '
        'Column155
        '
        Me.Column155.HeaderText = "Column155"
        Me.Column155.Name = "Column155"
        Me.Column155.ReadOnly = True
        '
        'Column156
        '
        Me.Column156.HeaderText = "Column156"
        Me.Column156.Name = "Column156"
        Me.Column156.ReadOnly = True
        '
        'Column157
        '
        Me.Column157.HeaderText = "Column157"
        Me.Column157.Name = "Column157"
        Me.Column157.ReadOnly = True
        '
        'Column158
        '
        Me.Column158.HeaderText = "Column158"
        Me.Column158.Name = "Column158"
        Me.Column158.ReadOnly = True
        '
        'Column159
        '
        Me.Column159.HeaderText = "Column159"
        Me.Column159.Name = "Column159"
        Me.Column159.ReadOnly = True
        '
        'Column160
        '
        Me.Column160.HeaderText = "Column160"
        Me.Column160.Name = "Column160"
        Me.Column160.ReadOnly = True
        '
        'Column161
        '
        Me.Column161.HeaderText = "Column161"
        Me.Column161.Name = "Column161"
        Me.Column161.ReadOnly = True
        '
        'Column162
        '
        Me.Column162.HeaderText = "Column162"
        Me.Column162.Name = "Column162"
        Me.Column162.ReadOnly = True
        '
        'Column163
        '
        Me.Column163.HeaderText = "Column163"
        Me.Column163.Name = "Column163"
        Me.Column163.ReadOnly = True
        '
        'Column164
        '
        Me.Column164.HeaderText = "Column164"
        Me.Column164.Name = "Column164"
        Me.Column164.ReadOnly = True
        '
        'DataGridViewSYSLOG2
        '
        Me.DataGridViewSYSLOG2.AllowUserToAddRows = False
        Me.DataGridViewSYSLOG2.AllowUserToDeleteRows = False
        Me.DataGridViewSYSLOG2.AllowUserToResizeColumns = False
        Me.DataGridViewSYSLOG2.AllowUserToResizeRows = False
        Me.DataGridViewSYSLOG2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewSYSLOG2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column17, Me.Column18, Me.Column19, Me.Column20, Me.Column21, Me.Column22, Me.Column23, Me.Column181})
        Me.DataGridViewSYSLOG2.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewSYSLOG2.Name = "DataGridViewSYSLOG2"
        Me.DataGridViewSYSLOG2.ReadOnly = True
        Me.DataGridViewSYSLOG2.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewSYSLOG2.TabIndex = 28
        '
        'Column17
        '
        Me.Column17.HeaderText = "Column17"
        Me.Column17.Name = "Column17"
        Me.Column17.ReadOnly = True
        '
        'Column18
        '
        Me.Column18.HeaderText = "Column18"
        Me.Column18.Name = "Column18"
        Me.Column18.ReadOnly = True
        '
        'Column19
        '
        Me.Column19.HeaderText = "Column19"
        Me.Column19.Name = "Column19"
        Me.Column19.ReadOnly = True
        '
        'Column20
        '
        Me.Column20.HeaderText = "Column20"
        Me.Column20.Name = "Column20"
        Me.Column20.ReadOnly = True
        '
        'Column21
        '
        Me.Column21.HeaderText = "Column21"
        Me.Column21.Name = "Column21"
        Me.Column21.ReadOnly = True
        '
        'Column22
        '
        Me.Column22.HeaderText = "Column22"
        Me.Column22.Name = "Column22"
        Me.Column22.ReadOnly = True
        '
        'Column23
        '
        Me.Column23.HeaderText = "Column23"
        Me.Column23.Name = "Column23"
        Me.Column23.ReadOnly = True
        '
        'Column181
        '
        Me.Column181.HeaderText = "Column181"
        Me.Column181.Name = "Column181"
        Me.Column181.ReadOnly = True
        '
        'DataGridViewLocusers
        '
        Me.DataGridViewLocusers.AllowUserToAddRows = False
        Me.DataGridViewLocusers.AllowUserToDeleteRows = False
        Me.DataGridViewLocusers.AllowUserToResizeColumns = False
        Me.DataGridViewLocusers.AllowUserToResizeRows = False
        Me.DataGridViewLocusers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewLocusers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column72, Me.Column73, Me.Column74, Me.Column75, Me.Column76, Me.Column77, Me.Column78, Me.Column79, Me.Column80, Me.Column81, Me.Column82, Me.Column83, Me.Column84, Me.Column85, Me.Column86, Me.Column87})
        Me.DataGridViewLocusers.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewLocusers.Name = "DataGridViewLocusers"
        Me.DataGridViewLocusers.ReadOnly = True
        Me.DataGridViewLocusers.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewLocusers.TabIndex = 26
        '
        'Column72
        '
        Me.Column72.HeaderText = "Column72"
        Me.Column72.Name = "Column72"
        Me.Column72.ReadOnly = True
        '
        'Column73
        '
        Me.Column73.HeaderText = "Column73"
        Me.Column73.Name = "Column73"
        Me.Column73.ReadOnly = True
        '
        'Column74
        '
        Me.Column74.HeaderText = "Column74"
        Me.Column74.Name = "Column74"
        Me.Column74.ReadOnly = True
        '
        'Column75
        '
        Me.Column75.HeaderText = "Column75"
        Me.Column75.Name = "Column75"
        Me.Column75.ReadOnly = True
        '
        'Column76
        '
        Me.Column76.HeaderText = "Column76"
        Me.Column76.Name = "Column76"
        Me.Column76.ReadOnly = True
        '
        'Column77
        '
        Me.Column77.HeaderText = "Column77"
        Me.Column77.Name = "Column77"
        Me.Column77.ReadOnly = True
        '
        'Column78
        '
        Me.Column78.HeaderText = "Column78"
        Me.Column78.Name = "Column78"
        Me.Column78.ReadOnly = True
        '
        'Column79
        '
        Me.Column79.HeaderText = "Column79"
        Me.Column79.Name = "Column79"
        Me.Column79.ReadOnly = True
        '
        'Column80
        '
        Me.Column80.HeaderText = "Column80"
        Me.Column80.Name = "Column80"
        Me.Column80.ReadOnly = True
        '
        'Column81
        '
        Me.Column81.HeaderText = "Column81"
        Me.Column81.Name = "Column81"
        Me.Column81.ReadOnly = True
        '
        'Column82
        '
        Me.Column82.HeaderText = "Column82"
        Me.Column82.Name = "Column82"
        Me.Column82.ReadOnly = True
        '
        'Column83
        '
        Me.Column83.HeaderText = "Column83"
        Me.Column83.Name = "Column83"
        Me.Column83.ReadOnly = True
        '
        'Column84
        '
        Me.Column84.HeaderText = "Column84"
        Me.Column84.Name = "Column84"
        Me.Column84.ReadOnly = True
        '
        'Column85
        '
        Me.Column85.HeaderText = "Column85"
        Me.Column85.Name = "Column85"
        Me.Column85.ReadOnly = True
        '
        'Column86
        '
        Me.Column86.HeaderText = "Column86"
        Me.Column86.Name = "Column86"
        Me.Column86.ReadOnly = True
        '
        'Column87
        '
        Me.Column87.HeaderText = "Column87"
        Me.Column87.Name = "Column87"
        Me.Column87.ReadOnly = True
        '
        'DataGridViewSYSLOG1
        '
        Me.DataGridViewSYSLOG1.AllowUserToAddRows = False
        Me.DataGridViewSYSLOG1.AllowUserToDeleteRows = False
        Me.DataGridViewSYSLOG1.AllowUserToResizeColumns = False
        Me.DataGridViewSYSLOG1.AllowUserToResizeRows = False
        Me.DataGridViewSYSLOG1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewSYSLOG1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column10, Me.Column11, Me.Column12, Me.Column13, Me.Column14, Me.Column15, Me.Column16, Me.Column180})
        Me.DataGridViewSYSLOG1.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewSYSLOG1.Name = "DataGridViewSYSLOG1"
        Me.DataGridViewSYSLOG1.ReadOnly = True
        Me.DataGridViewSYSLOG1.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewSYSLOG1.TabIndex = 28
        '
        'Column10
        '
        Me.Column10.HeaderText = "Column10"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        '
        'Column11
        '
        Me.Column11.HeaderText = "Column11"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        '
        'Column12
        '
        Me.Column12.HeaderText = "Column12"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        '
        'Column13
        '
        Me.Column13.HeaderText = "Column13"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        '
        'Column14
        '
        Me.Column14.HeaderText = "Column14"
        Me.Column14.Name = "Column14"
        Me.Column14.ReadOnly = True
        '
        'Column15
        '
        Me.Column15.HeaderText = "Column15"
        Me.Column15.Name = "Column15"
        Me.Column15.ReadOnly = True
        '
        'Column16
        '
        Me.Column16.HeaderText = "Column16"
        Me.Column16.Name = "Column16"
        Me.Column16.ReadOnly = True
        '
        'Column180
        '
        Me.Column180.HeaderText = "Column180"
        Me.Column180.Name = "Column180"
        Me.Column180.ReadOnly = True
        '
        'DataGridViewTRANDET
        '
        Me.DataGridViewTRANDET.AllowUserToAddRows = False
        Me.DataGridViewTRANDET.AllowUserToDeleteRows = False
        Me.DataGridViewTRANDET.AllowUserToResizeColumns = False
        Me.DataGridViewTRANDET.AllowUserToResizeRows = False
        Me.DataGridViewTRANDET.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewTRANDET.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column137, Me.Column115, Me.Column138, Me.Column139, Me.Column140, Me.Column141, Me.Column142, Me.Column143, Me.Column144, Me.Column145, Me.Column146, Me.Column147, Me.Column148, Me.Column149, Me.Column150, Me.Column151, Me.Column152, Me.Column153, Me.Column154})
        Me.DataGridViewTRANDET.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewTRANDET.Name = "DataGridViewTRANDET"
        Me.DataGridViewTRANDET.ReadOnly = True
        Me.DataGridViewTRANDET.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewTRANDET.TabIndex = 4
        '
        'Column137
        '
        Me.Column137.HeaderText = "Column137"
        Me.Column137.Name = "Column137"
        Me.Column137.ReadOnly = True
        '
        'Column115
        '
        Me.Column115.HeaderText = "Column115"
        Me.Column115.Name = "Column115"
        Me.Column115.ReadOnly = True
        '
        'Column138
        '
        Me.Column138.HeaderText = "Column138"
        Me.Column138.Name = "Column138"
        Me.Column138.ReadOnly = True
        '
        'Column139
        '
        Me.Column139.HeaderText = "Column139"
        Me.Column139.Name = "Column139"
        Me.Column139.ReadOnly = True
        '
        'Column140
        '
        Me.Column140.HeaderText = "Column140"
        Me.Column140.Name = "Column140"
        Me.Column140.ReadOnly = True
        '
        'Column141
        '
        Me.Column141.HeaderText = "Column141"
        Me.Column141.Name = "Column141"
        Me.Column141.ReadOnly = True
        '
        'Column142
        '
        Me.Column142.HeaderText = "Column142"
        Me.Column142.Name = "Column142"
        Me.Column142.ReadOnly = True
        '
        'Column143
        '
        Me.Column143.HeaderText = "Column143"
        Me.Column143.Name = "Column143"
        Me.Column143.ReadOnly = True
        '
        'Column144
        '
        Me.Column144.HeaderText = "Column144"
        Me.Column144.Name = "Column144"
        Me.Column144.ReadOnly = True
        '
        'Column145
        '
        Me.Column145.HeaderText = "Column145"
        Me.Column145.Name = "Column145"
        Me.Column145.ReadOnly = True
        '
        'Column146
        '
        Me.Column146.HeaderText = "Column146"
        Me.Column146.Name = "Column146"
        Me.Column146.ReadOnly = True
        '
        'Column147
        '
        Me.Column147.HeaderText = "Column147"
        Me.Column147.Name = "Column147"
        Me.Column147.ReadOnly = True
        '
        'Column148
        '
        Me.Column148.HeaderText = "Column148"
        Me.Column148.Name = "Column148"
        Me.Column148.ReadOnly = True
        '
        'Column149
        '
        Me.Column149.HeaderText = "Column149"
        Me.Column149.Name = "Column149"
        Me.Column149.ReadOnly = True
        '
        'Column150
        '
        Me.Column150.HeaderText = "Column150"
        Me.Column150.Name = "Column150"
        Me.Column150.ReadOnly = True
        '
        'Column151
        '
        Me.Column151.HeaderText = "Column151"
        Me.Column151.Name = "Column151"
        Me.Column151.ReadOnly = True
        '
        'Column152
        '
        Me.Column152.HeaderText = "Column152"
        Me.Column152.Name = "Column152"
        Me.Column152.ReadOnly = True
        '
        'Column153
        '
        Me.Column153.HeaderText = "Column153"
        Me.Column153.Name = "Column153"
        Me.Column153.ReadOnly = True
        '
        'Column154
        '
        Me.Column154.HeaderText = "Column154"
        Me.Column154.Name = "Column154"
        Me.Column154.ReadOnly = True
        '
        'DataGridViewCUSTOMPRODUCTS
        '
        Me.DataGridViewCUSTOMPRODUCTS.AllowUserToAddRows = False
        Me.DataGridViewCUSTOMPRODUCTS.AllowUserToDeleteRows = False
        Me.DataGridViewCUSTOMPRODUCTS.AllowUserToResizeColumns = False
        Me.DataGridViewCUSTOMPRODUCTS.AllowUserToResizeRows = False
        Me.DataGridViewCUSTOMPRODUCTS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewCUSTOMPRODUCTS.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column42, Me.Column43, Me.Column44, Me.Column45, Me.Column46, Me.Column47, Me.Column48, Me.Column49, Me.Column50, Me.Column51, Me.Column52, Me.Column53, Me.Column54, Me.Column55, Me.Column56, Me.Column57, Me.Column58})
        Me.DataGridViewCUSTOMPRODUCTS.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewCUSTOMPRODUCTS.Name = "DataGridViewCUSTOMPRODUCTS"
        Me.DataGridViewCUSTOMPRODUCTS.ReadOnly = True
        Me.DataGridViewCUSTOMPRODUCTS.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewCUSTOMPRODUCTS.TabIndex = 43
        '
        'Column42
        '
        Me.Column42.HeaderText = "Column42"
        Me.Column42.Name = "Column42"
        Me.Column42.ReadOnly = True
        '
        'Column43
        '
        Me.Column43.HeaderText = "Column43"
        Me.Column43.Name = "Column43"
        Me.Column43.ReadOnly = True
        '
        'Column44
        '
        Me.Column44.HeaderText = "Column44"
        Me.Column44.Name = "Column44"
        Me.Column44.ReadOnly = True
        '
        'Column45
        '
        Me.Column45.HeaderText = "Column45"
        Me.Column45.Name = "Column45"
        Me.Column45.ReadOnly = True
        '
        'Column46
        '
        Me.Column46.HeaderText = "Column46"
        Me.Column46.Name = "Column46"
        Me.Column46.ReadOnly = True
        '
        'Column47
        '
        Me.Column47.HeaderText = "Column47"
        Me.Column47.Name = "Column47"
        Me.Column47.ReadOnly = True
        '
        'Column48
        '
        Me.Column48.HeaderText = "Column48"
        Me.Column48.Name = "Column48"
        Me.Column48.ReadOnly = True
        '
        'Column49
        '
        Me.Column49.HeaderText = "Column49"
        Me.Column49.Name = "Column49"
        Me.Column49.ReadOnly = True
        '
        'Column50
        '
        Me.Column50.HeaderText = "Column50"
        Me.Column50.Name = "Column50"
        Me.Column50.ReadOnly = True
        '
        'Column51
        '
        Me.Column51.HeaderText = "Column51"
        Me.Column51.Name = "Column51"
        Me.Column51.ReadOnly = True
        '
        'Column52
        '
        Me.Column52.HeaderText = "Column52"
        Me.Column52.Name = "Column52"
        Me.Column52.ReadOnly = True
        '
        'Column53
        '
        Me.Column53.HeaderText = "Column53"
        Me.Column53.Name = "Column53"
        Me.Column53.ReadOnly = True
        '
        'Column54
        '
        Me.Column54.HeaderText = "Column54"
        Me.Column54.Name = "Column54"
        Me.Column54.ReadOnly = True
        '
        'Column55
        '
        Me.Column55.HeaderText = "Column55"
        Me.Column55.Name = "Column55"
        Me.Column55.ReadOnly = True
        '
        'Column56
        '
        Me.Column56.HeaderText = "Column56"
        Me.Column56.Name = "Column56"
        Me.Column56.ReadOnly = True
        '
        'Column57
        '
        Me.Column57.HeaderText = "Column57"
        Me.Column57.Name = "Column57"
        Me.Column57.ReadOnly = True
        '
        'Column58
        '
        Me.Column58.HeaderText = "Column58"
        Me.Column58.Name = "Column58"
        Me.Column58.ReadOnly = True
        '
        'DataGridViewSYSLOG4
        '
        Me.DataGridViewSYSLOG4.AllowUserToAddRows = False
        Me.DataGridViewSYSLOG4.AllowUserToDeleteRows = False
        Me.DataGridViewSYSLOG4.AllowUserToResizeColumns = False
        Me.DataGridViewSYSLOG4.AllowUserToResizeRows = False
        Me.DataGridViewSYSLOG4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewSYSLOG4.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8, Me.Column9, Me.Column183})
        Me.DataGridViewSYSLOG4.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewSYSLOG4.Name = "DataGridViewSYSLOG4"
        Me.DataGridViewSYSLOG4.ReadOnly = True
        Me.DataGridViewSYSLOG4.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewSYSLOG4.TabIndex = 28
        '
        'Column3
        '
        Me.Column3.HeaderText = "Column3"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "Column4"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Column5"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "Column6"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "Column7"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.HeaderText = "Column8"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "Column9"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'Column183
        '
        Me.Column183.HeaderText = "Column183"
        Me.Column183.Name = "Column183"
        Me.Column183.ReadOnly = True
        '
        'DataGridViewSYSLOG3
        '
        Me.DataGridViewSYSLOG3.AllowUserToAddRows = False
        Me.DataGridViewSYSLOG3.AllowUserToDeleteRows = False
        Me.DataGridViewSYSLOG3.AllowUserToResizeColumns = False
        Me.DataGridViewSYSLOG3.AllowUserToResizeRows = False
        Me.DataGridViewSYSLOG3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewSYSLOG3.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column24, Me.Column25, Me.Column26, Me.Column27, Me.Column28, Me.Column29, Me.Column30, Me.Column182})
        Me.DataGridViewSYSLOG3.Location = New System.Drawing.Point(6, 19)
        Me.DataGridViewSYSLOG3.Name = "DataGridViewSYSLOG3"
        Me.DataGridViewSYSLOG3.ReadOnly = True
        Me.DataGridViewSYSLOG3.Size = New System.Drawing.Size(230, 80)
        Me.DataGridViewSYSLOG3.TabIndex = 28
        '
        'Column24
        '
        Me.Column24.HeaderText = "Column24"
        Me.Column24.Name = "Column24"
        Me.Column24.ReadOnly = True
        '
        'Column25
        '
        Me.Column25.HeaderText = "Column25"
        Me.Column25.Name = "Column25"
        Me.Column25.ReadOnly = True
        '
        'Column26
        '
        Me.Column26.HeaderText = "Column26"
        Me.Column26.Name = "Column26"
        Me.Column26.ReadOnly = True
        '
        'Column27
        '
        Me.Column27.HeaderText = "Column27"
        Me.Column27.Name = "Column27"
        Me.Column27.ReadOnly = True
        '
        'Column28
        '
        Me.Column28.HeaderText = "Column28"
        Me.Column28.Name = "Column28"
        Me.Column28.ReadOnly = True
        '
        'Column29
        '
        Me.Column29.HeaderText = "Column29"
        Me.Column29.Name = "Column29"
        Me.Column29.ReadOnly = True
        '
        'Column30
        '
        Me.Column30.HeaderText = "Column30"
        Me.Column30.Name = "Column30"
        Me.Column30.ReadOnly = True
        '
        'Column182
        '
        Me.Column182.HeaderText = "Column182"
        Me.Column182.Name = "Column182"
        Me.Column182.ReadOnly = True
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'BackgroundWorkerFILLDATAGRIDS
        '
        Me.BackgroundWorkerFILLDATAGRIDS.WorkerReportsProgress = True
        Me.BackgroundWorkerFILLDATAGRIDS.WorkerSupportsCancellation = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.RadioButton2)
        Me.Panel1.Controls.Add(Me.RadioButton1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 229)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(492, 87)
        Me.Panel1.TabIndex = 59
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(3, 49)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(485, 35)
        Me.Button2.TabIndex = 43
        Me.Button2.Text = "Start"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton2.Location = New System.Drawing.Point(246, 18)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(183, 23)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.Text = "UPLOAD INVENTORY"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton1.Location = New System.Drawing.Point(52, 18)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(188, 23)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "UPLOAD SALES DATA"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 316)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(492, 10)
        Me.Panel3.TabIndex = 61
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LabelSeniorDetailsItem, 2, 15)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelSeniorDetailsTime, 1, 15)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelSeniorDetails, 0, 15)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelErrorItem, 2, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelACCItem, 2, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelCouponItem, 2, 13)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelRETItem, 2, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelPRICEREQItem, 2, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelCPRODItem, 2, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelDEPOSITItem, 2, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelMODETItem, 2, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelSYS1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelEXPDItem, 2, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelSYS1Item, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelEXPItem, 2, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelZREADINVITEM, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelDTransac, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelDTransactD, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelINVItem, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelRET, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelINV, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelRETTime, 1, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelDTransactDItem, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelZREADINVTIME, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelDTransacItem, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelZREADINV, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelErrorTime, 1, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelEXP, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelEXPD, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelACC, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelCPRODTime, 1, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelCPROD, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelCouponTime, 1, 13)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelMODET, 0, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelMODETTime, 1, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelDEPOSIT, 0, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelPRICEREQTime, 1, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelError, 0, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelDEPOSITTime, 1, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelPRICEREQ, 0, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelCoupon, 0, 13)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelACCTime, 1, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelSYS1Time, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelEXPDTime, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelDTransacTime, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelDTransactDTime, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelINVTime, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelEXPTime, 1, 5)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(-1, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 16
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(492, 227)
        Me.TableLayoutPanel1.TabIndex = 62
        '
        'LabelSeniorDetailsItem
        '
        Me.LabelSeniorDetailsItem.AutoSize = True
        Me.LabelSeniorDetailsItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelSeniorDetailsItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSeniorDetailsItem.Location = New System.Drawing.Point(475, 210)
        Me.LabelSeniorDetailsItem.Name = "LabelSeniorDetailsItem"
        Me.LabelSeniorDetailsItem.Size = New System.Drawing.Size(14, 17)
        Me.LabelSeniorDetailsItem.TabIndex = 54
        Me.LabelSeniorDetailsItem.Text = "0"
        '
        'LabelSeniorDetailsTime
        '
        Me.LabelSeniorDetailsTime.AutoSize = True
        Me.LabelSeniorDetailsTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSeniorDetailsTime.Location = New System.Drawing.Point(175, 210)
        Me.LabelSeniorDetailsTime.Name = "LabelSeniorDetailsTime"
        Me.LabelSeniorDetailsTime.Size = New System.Drawing.Size(165, 14)
        Me.LabelSeniorDetailsTime.TabIndex = 58
        Me.LabelSeniorDetailsTime.Text = "Estimating Time. Please Wait"
        '
        'LabelSeniorDetails
        '
        Me.LabelSeniorDetails.AutoSize = True
        Me.LabelSeniorDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSeniorDetails.Location = New System.Drawing.Point(3, 210)
        Me.LabelSeniorDetails.Name = "LabelSeniorDetails"
        Me.LabelSeniorDetails.Size = New System.Drawing.Size(93, 14)
        Me.LabelSeniorDetails.TabIndex = 57
        Me.LabelSeniorDetails.Text = "Discount Details"
        '
        'SynctoCloud
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(492, 357)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.ButtonSYNCINVENTORY)
        Me.Controls.Add(Me.ButtonSYNCDATA)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SynctoCloud"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "POS | SYNC DATA"
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewEXPDET, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.DataGridViewINV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewEXP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewTRAN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DataGridViewZREADINVENTORY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DatagridviewSenior, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewERRORS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewCoupons, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewPriceChangeRequest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewDepositSlip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewMODEOFTRANSACTION, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewRetrefdetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewSYSLOG2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewLocusers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewSYSLOG1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewTRANDET, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewCUSTOMPRODUCTS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewSYSLOG4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewSYSLOG3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BackgroundWorkerSYNCTOCLOUD As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer1 As Timer
    Friend WithEvents DataGridViewEXP As DataGridView
    Friend WithEvents DataGridViewINV As DataGridView
    Friend WithEvents DataGridViewTRAN As DataGridView
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents DataGridViewEXPDET As DataGridView
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents Panel7 As Panel
    Friend WithEvents ButtonSYNCDATA As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LabelTTLRowtoSync As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents DataGridViewLocusers As DataGridView
    Friend WithEvents LabelTime As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LabelRowtoSync As Label
    Friend WithEvents LabelACC As Label
    Friend WithEvents LabelEXPD As Label
    Friend WithEvents LabelEXP As Label
    Friend WithEvents LabelINV As Label
    Friend WithEvents LabelDTransactD As Label
    Friend WithEvents LabelDTransac As Label
    Friend WithEvents LabelACCTime As Label
    Friend WithEvents LabelEXPDTime As Label
    Friend WithEvents LabelEXPTime As Label
    Friend WithEvents LabelINVTime As Label
    Friend WithEvents LabelDTransactDTime As Label
    Friend WithEvents LabelDTransacTime As Label
    Friend WithEvents LabelSYS1Time As Label
    Friend WithEvents LabelSYS1 As Label
    Friend WithEvents LabelSYS1Item As Label
    Friend WithEvents LabelACCItem As Label
    Friend WithEvents LabelEXPDItem As Label
    Friend WithEvents LabelEXPItem As Label
    Friend WithEvents LabelINVItem As Label
    Friend WithEvents LabelDTransactDItem As Label
    Friend WithEvents LabelDTransacItem As Label
    Friend WithEvents DataGridViewSYSLOG2 As DataGridView
    Friend WithEvents DataGridViewSYSLOG4 As DataGridView
    Friend WithEvents DataGridViewSYSLOG3 As DataGridView
    Friend WithEvents DataGridViewSYSLOG1 As DataGridView
    Friend WithEvents DataGridViewTRANDET As DataGridView
    Friend WithEvents DataGridViewRetrefdetails As DataGridView
    Friend WithEvents LabelRETItem As Label
    Friend WithEvents LabelRETTime As Label
    Friend WithEvents LabelRET As Label
    Friend WithEvents DataGridViewCUSTOMPRODUCTS As DataGridView
    Friend WithEvents LabelCPRODItem As Label
    Friend WithEvents LabelCPRODTime As Label
    Friend WithEvents LabelCPROD As Label
    Friend WithEvents DataGridViewMODEOFTRANSACTION As DataGridView
    Friend WithEvents LabelMODETItem As Label
    Friend WithEvents LabelMODETTime As Label
    Friend WithEvents LabelMODET As Label
    Friend WithEvents LabelDEPOSITItem As Label
    Friend WithEvents LabelDEPOSITTime As Label
    Friend WithEvents LabelDEPOSIT As Label
    Friend WithEvents DataGridViewDepositSlip As DataGridView
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents Column11 As DataGridViewTextBoxColumn
    Friend WithEvents Column12 As DataGridViewTextBoxColumn
    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Column14 As DataGridViewTextBoxColumn
    Friend WithEvents Column15 As DataGridViewTextBoxColumn
    Friend WithEvents Column16 As DataGridViewTextBoxColumn
    Friend WithEvents Column17 As DataGridViewTextBoxColumn
    Friend WithEvents Column18 As DataGridViewTextBoxColumn
    Friend WithEvents Column19 As DataGridViewTextBoxColumn
    Friend WithEvents Column20 As DataGridViewTextBoxColumn
    Friend WithEvents Column21 As DataGridViewTextBoxColumn
    Friend WithEvents Column22 As DataGridViewTextBoxColumn
    Friend WithEvents Column23 As DataGridViewTextBoxColumn
    Friend WithEvents Column24 As DataGridViewTextBoxColumn
    Friend WithEvents Column25 As DataGridViewTextBoxColumn
    Friend WithEvents Column26 As DataGridViewTextBoxColumn
    Friend WithEvents Column27 As DataGridViewTextBoxColumn
    Friend WithEvents Column28 As DataGridViewTextBoxColumn
    Friend WithEvents Column29 As DataGridViewTextBoxColumn
    Friend WithEvents Column30 As DataGridViewTextBoxColumn
    Friend WithEvents Column31 As DataGridViewTextBoxColumn
    Friend WithEvents Column32 As DataGridViewTextBoxColumn
    Friend WithEvents Column33 As DataGridViewTextBoxColumn
    Friend WithEvents Column34 As DataGridViewTextBoxColumn
    Friend WithEvents Column35 As DataGridViewTextBoxColumn
    Friend WithEvents Column36 As DataGridViewTextBoxColumn
    Friend WithEvents Column37 As DataGridViewTextBoxColumn
    Friend WithEvents Column38 As DataGridViewTextBoxColumn
    Friend WithEvents Column39 As DataGridViewTextBoxColumn
    Friend WithEvents Column40 As DataGridViewTextBoxColumn
    Friend WithEvents Column41 As DataGridViewTextBoxColumn
    Friend WithEvents Column42 As DataGridViewTextBoxColumn
    Friend WithEvents Column43 As DataGridViewTextBoxColumn
    Friend WithEvents Column44 As DataGridViewTextBoxColumn
    Friend WithEvents Column45 As DataGridViewTextBoxColumn
    Friend WithEvents Column46 As DataGridViewTextBoxColumn
    Friend WithEvents Column47 As DataGridViewTextBoxColumn
    Friend WithEvents Column48 As DataGridViewTextBoxColumn
    Friend WithEvents Column49 As DataGridViewTextBoxColumn
    Friend WithEvents Column50 As DataGridViewTextBoxColumn
    Friend WithEvents Column51 As DataGridViewTextBoxColumn
    Friend WithEvents Column52 As DataGridViewTextBoxColumn
    Friend WithEvents Column53 As DataGridViewTextBoxColumn
    Friend WithEvents Column54 As DataGridViewTextBoxColumn
    Friend WithEvents Column55 As DataGridViewTextBoxColumn
    Friend WithEvents Column56 As DataGridViewTextBoxColumn
    Friend WithEvents Column57 As DataGridViewTextBoxColumn
    Friend WithEvents Column58 As DataGridViewTextBoxColumn
    Friend WithEvents Column59 As DataGridViewTextBoxColumn
    Friend WithEvents Column60 As DataGridViewTextBoxColumn
    Friend WithEvents Column61 As DataGridViewTextBoxColumn
    Friend WithEvents Column62 As DataGridViewTextBoxColumn
    Friend WithEvents Column63 As DataGridViewTextBoxColumn
    Friend WithEvents Column64 As DataGridViewTextBoxColumn
    Friend WithEvents Column65 As DataGridViewTextBoxColumn
    Friend WithEvents Column66 As DataGridViewTextBoxColumn
    Friend WithEvents Column67 As DataGridViewTextBoxColumn
    Friend WithEvents Column68 As DataGridViewTextBoxColumn
    Friend WithEvents Column69 As DataGridViewTextBoxColumn
    Friend WithEvents Column70 As DataGridViewTextBoxColumn
    Friend WithEvents Column71 As DataGridViewTextBoxColumn
    Friend WithEvents Column72 As DataGridViewTextBoxColumn
    Friend WithEvents Column73 As DataGridViewTextBoxColumn
    Friend WithEvents Column74 As DataGridViewTextBoxColumn
    Friend WithEvents Column75 As DataGridViewTextBoxColumn
    Friend WithEvents Column76 As DataGridViewTextBoxColumn
    Friend WithEvents Column77 As DataGridViewTextBoxColumn
    Friend WithEvents Column78 As DataGridViewTextBoxColumn
    Friend WithEvents Column79 As DataGridViewTextBoxColumn
    Friend WithEvents Column80 As DataGridViewTextBoxColumn
    Friend WithEvents Column81 As DataGridViewTextBoxColumn
    Friend WithEvents Column82 As DataGridViewTextBoxColumn
    Friend WithEvents Column83 As DataGridViewTextBoxColumn
    Friend WithEvents Column84 As DataGridViewTextBoxColumn
    Friend WithEvents Column85 As DataGridViewTextBoxColumn
    Friend WithEvents Column86 As DataGridViewTextBoxColumn
    Friend WithEvents Column87 As DataGridViewTextBoxColumn
    Friend WithEvents Column88 As DataGridViewTextBoxColumn
    Friend WithEvents Column89 As DataGridViewTextBoxColumn
    Friend WithEvents Column90 As DataGridViewTextBoxColumn
    Friend WithEvents Column91 As DataGridViewTextBoxColumn
    Friend WithEvents Column92 As DataGridViewTextBoxColumn
    Friend WithEvents Column93 As DataGridViewTextBoxColumn
    Friend WithEvents Column94 As DataGridViewTextBoxColumn
    Friend WithEvents Column95 As DataGridViewTextBoxColumn
    Friend WithEvents Column96 As DataGridViewTextBoxColumn
    Friend WithEvents Column97 As DataGridViewTextBoxColumn
    Friend WithEvents Column98 As DataGridViewTextBoxColumn
    Friend WithEvents Column99 As DataGridViewTextBoxColumn
    Friend WithEvents Column100 As DataGridViewTextBoxColumn
    Friend WithEvents Column101 As DataGridViewTextBoxColumn
    Friend WithEvents Column102 As DataGridViewTextBoxColumn
    Friend WithEvents Column103 As DataGridViewTextBoxColumn
    Friend WithEvents Column104 As DataGridViewTextBoxColumn
    Friend WithEvents Column105 As DataGridViewTextBoxColumn
    Friend WithEvents Column106 As DataGridViewTextBoxColumn
    Friend WithEvents Column107 As DataGridViewTextBoxColumn
    Friend WithEvents Column108 As DataGridViewTextBoxColumn
    Friend WithEvents Column109 As DataGridViewTextBoxColumn
    Friend WithEvents Column110 As DataGridViewTextBoxColumn
    Friend WithEvents Column111 As DataGridViewTextBoxColumn
    Friend WithEvents Column112 As DataGridViewTextBoxColumn
    Friend WithEvents Column113 As DataGridViewTextBoxColumn
    Friend WithEvents Column114 As DataGridViewTextBoxColumn
    Friend WithEvents Column155 As DataGridViewTextBoxColumn
    Friend WithEvents Column156 As DataGridViewTextBoxColumn
    Friend WithEvents Column157 As DataGridViewTextBoxColumn
    Friend WithEvents Column158 As DataGridViewTextBoxColumn
    Friend WithEvents Column159 As DataGridViewTextBoxColumn
    Friend WithEvents Column160 As DataGridViewTextBoxColumn
    Friend WithEvents Column161 As DataGridViewTextBoxColumn
    Friend WithEvents Column162 As DataGridViewTextBoxColumn
    Friend WithEvents Column163 As DataGridViewTextBoxColumn
    Friend WithEvents Column164 As DataGridViewTextBoxColumn
    Friend WithEvents Column165 As DataGridViewTextBoxColumn
    Friend WithEvents Column166 As DataGridViewTextBoxColumn
    Friend WithEvents Column167 As DataGridViewTextBoxColumn
    Friend WithEvents Column168 As DataGridViewTextBoxColumn
    Friend WithEvents Column169 As DataGridViewTextBoxColumn
    Friend WithEvents Column170 As DataGridViewTextBoxColumn
    Friend WithEvents Column171 As DataGridViewTextBoxColumn
    Friend WithEvents Column172 As DataGridViewTextBoxColumn
    Friend WithEvents Column173 As DataGridViewTextBoxColumn
    Friend WithEvents Column174 As DataGridViewTextBoxColumn
    Friend WithEvents Column175 As DataGridViewTextBoxColumn
    Friend WithEvents Column176 As DataGridViewTextBoxColumn
    Friend WithEvents Column177 As DataGridViewTextBoxColumn
    Friend WithEvents Column178 As DataGridViewTextBoxColumn
    Friend WithEvents Column179 As DataGridViewTextBoxColumn
    Friend WithEvents Column181 As DataGridViewTextBoxColumn
    Friend WithEvents Column182 As DataGridViewTextBoxColumn
    Friend WithEvents Column183 As DataGridViewTextBoxColumn
    Friend WithEvents Column180 As DataGridViewTextBoxColumn
    Friend WithEvents Column137 As DataGridViewTextBoxColumn
    Friend WithEvents Column115 As DataGridViewTextBoxColumn
    Friend WithEvents Column138 As DataGridViewTextBoxColumn
    Friend WithEvents Column139 As DataGridViewTextBoxColumn
    Friend WithEvents Column140 As DataGridViewTextBoxColumn
    Friend WithEvents Column141 As DataGridViewTextBoxColumn
    Friend WithEvents Column142 As DataGridViewTextBoxColumn
    Friend WithEvents Column143 As DataGridViewTextBoxColumn
    Friend WithEvents Column144 As DataGridViewTextBoxColumn
    Friend WithEvents Column145 As DataGridViewTextBoxColumn
    Friend WithEvents Column146 As DataGridViewTextBoxColumn
    Friend WithEvents Column147 As DataGridViewTextBoxColumn
    Friend WithEvents Column148 As DataGridViewTextBoxColumn
    Friend WithEvents Column149 As DataGridViewTextBoxColumn
    Friend WithEvents Column150 As DataGridViewTextBoxColumn
    Friend WithEvents Column151 As DataGridViewTextBoxColumn
    Friend WithEvents Column152 As DataGridViewTextBoxColumn
    Friend WithEvents Column153 As DataGridViewTextBoxColumn
    Friend WithEvents Column154 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewPriceChangeRequest As DataGridView
    Friend WithEvents LabelPRICEREQ As Label
    Friend WithEvents LabelPRICEREQItem As Label
    Friend WithEvents LabelPRICEREQTime As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Timer2 As Timer
    Friend WithEvents DataGridViewCoupons As DataGridView
    Friend WithEvents LabelCoupon As Label
    Friend WithEvents LabelCouponItem As Label
    Friend WithEvents LabelCouponTime As Label
    Friend WithEvents BackgroundWorkerFILLDATAGRIDS As System.ComponentModel.BackgroundWorker
    Friend WithEvents ButtonSYNCINVENTORY As Button
    Friend WithEvents LabelErrorItem As Label
    Friend WithEvents LabelErrorTime As Label
    Friend WithEvents LabelError As Label
    Friend WithEvents DataGridViewERRORS As DataGridView
    Friend WithEvents DataGridViewZREADINVENTORY As DataGridView
    Friend WithEvents LabelZREADINVITEM As Label
    Friend WithEvents LabelZREADINVTIME As Label
    Friend WithEvents LabelZREADINV As Label
    Friend WithEvents Column116 As DataGridViewTextBoxColumn
    Friend WithEvents Column117 As DataGridViewTextBoxColumn
    Friend WithEvents Column118 As DataGridViewTextBoxColumn
    Friend WithEvents Column119 As DataGridViewTextBoxColumn
    Friend WithEvents Column120 As DataGridViewTextBoxColumn
    Friend WithEvents Column121 As DataGridViewTextBoxColumn
    Friend WithEvents Column122 As DataGridViewTextBoxColumn
    Friend WithEvents Column123 As DataGridViewTextBoxColumn
    Friend WithEvents Column124 As DataGridViewTextBoxColumn
    Friend WithEvents Column125 As DataGridViewTextBoxColumn
    Friend WithEvents Column126 As DataGridViewTextBoxColumn
    Friend WithEvents Column127 As DataGridViewTextBoxColumn
    Friend WithEvents Column128 As DataGridViewTextBoxColumn
    Friend WithEvents Column129 As DataGridViewTextBoxColumn
    Friend WithEvents Column130 As DataGridViewTextBoxColumn
    Friend WithEvents Column131 As DataGridViewTextBoxColumn
    Friend WithEvents Column132 As DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents Panel3 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LabelSeniorDetailsItem As Label
    Friend WithEvents LabelSeniorDetailsTime As Label
    Friend WithEvents LabelSeniorDetails As Label
    Friend WithEvents DatagridviewSenior As DataGridView
End Class
