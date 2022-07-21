using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DashboarLaboral.Data
{
    public partial class insitedb : DbContext
    {
        public insitedb()
        {
        }

        public insitedb(DbContextOptions<insitedb> options)
            : base(options)
        {
        }

        public virtual DbSet<Ausentismo> Ausentismos { get; set; }
        public virtual DbSet<Ejecucione> Ejecuciones { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Empresa2> Empresa2s { get; set; }
        public virtual DbSet<Horario> Horarios { get; set; }
        public virtual DbSet<Parametro> Parametros { get; set; }
        public virtual DbSet<PosicionesOffPremise> PosicionesOffPremises { get; set; }
        public virtual DbSet<ParametroCorreos> ParametroCorreos { get; set; }
        public virtual DbSet<PosicionOffPremiseHeader> PosicionOffpremiseHeader { get; set; }
        public virtual DbSet<PosicionOffPremiseDetails> PosicionOffpremiseDetails { get; set; }
        public virtual DbSet<EventLog> LogEvents { get; set; }
        public virtual DbSet<PosicionesFeriados> PosicionesFeriados { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Ausentismo>(entity =>
            {
                entity.ToTable("Ausentismo");
                entity.HasKey(e => e.Aucod);

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Aucod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("AUCOD");

                entity.Property(e => e.Audes)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("AUDES");

                entity.Property(e => e.Aujus)
                    .HasColumnType("bit")
                    .HasColumnName("AUJUS");

                entity.Property(e => e.Autel)
                    .HasColumnName("AUTEL");

                entity.Property(e => e.Cuarentena)
                    .HasColumnName("CUARENTENA");

                entity.Property(e => e.Riesgo)
                    .HasColumnName("RIESGO");
            });

            modelBuilder.Entity<Ejecucione>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Idejecucion).HasColumnName("IDEjecucion");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.CodigoEmpresa);

                entity.ToTable("Empresa");

                entity.Property(e => e.CodigoEmpresa).HasMaxLength(50);

                entity.Property(e => e.Color)
                    .HasMaxLength(50);

                entity.Property(e => e.RowId)
                    .IsRequired();

                entity.Property(e => e.Empresa1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Empresa");
            });

            modelBuilder.Entity<Empresa2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Empresa2");

                entity.Property(e => e.CodigoEmpresa).HasMaxLength(50);

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Empresa)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Horario>(entity =>
            {
                entity.HasKey(e => e.Clave);

                entity.ToView("HorarioView");

                entity
                    .Ignore(e => e.Prioridad);

                entity.HasIndex(e => e.Clave, "nci_wi_Horario_AA019C99734C38649AC140A2EB3B7D33");

                entity.Property(e => e.Administrativo).HasColumnName("ADMINISTRATIVO");

                entity.Property(e => e.Aucod)
                    .HasMaxLength(4)
                    .HasColumnName("AUCOD");

                entity.Property(e => e.Canthoraspermiso)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("CANTHORASPERMISO");

                entity.Property(e => e.CANTHoras)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("CANTHoras");

                entity.Property(e => e.HorasFuera)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("HORASFUERAS");
                
                entity.Property(e => e.Clave)
                    .HasMaxLength(20)
                    .HasColumnName("CLAVE");

                entity.Property(e => e.Codigoempleado).HasColumnName("Codigoempleado");

                entity.Property(e => e.Codigohorario)
                    .HasMaxLength(4)
                    .HasColumnName("CODIGOHORARIO");

                entity.Property(e => e.Codigosupervisor).HasColumnName("CODIGOSUPERVISOR");

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .HasColumnName("CORREO");

                entity.Property(e => e.Correosupervisor)
                    .HasMaxLength(200)
                    .HasColumnName("CORREOSUPERVISOR");

                entity.Property(e => e.Corrida)
                    .HasColumnType("datetime")
                    .HasColumnName("CORRIDA");

                entity.Property(e => e.Departamento)
                    .HasMaxLength(200)
                    .HasColumnName("DEPARTAMENTO");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(10)
                    .HasColumnName("EMPRESA");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA");


                entity.Property(e => e.Fechafin)
                    .HasColumnType("date")
                    .HasColumnName("FECHAFIN");

                entity.Property(e => e.Fechaini)
                    .HasColumnType("date")
                    .HasColumnName("FECHAINI");

                entity.Property(e => e.Horafin)
                    .HasColumnType("datetime")
                    .HasColumnName("HORAFIN");

                entity.Property(e => e.Horaini)
                    .HasColumnType("datetime")
                    .HasColumnName("HORAINI");

                entity.Property(e => e.Horasdescontadas)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("HORASDESCONTADAS");

                entity.Property(e => e.Horasextras)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("HORASEXTRAS");

                entity.Property(e => e.HorasextrasPla)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("HORASEXTRASPLA");

                entity.Property(e => e.Nombrecompleto)
                    .HasMaxLength(200)
                    .HasColumnName("NOMBRECOMPLETO");

                entity.Property(e => e.Nombresupervisor)
                    .HasMaxLength(200)
                    .HasColumnName("NOMBRESUPERVISOR");

                entity.Property(e => e.Poncheentrada)
                    .HasColumnType("datetime")
                    .HasColumnName("PONCHEENTRADA");

                entity.Property(e => e.Ponchesalida)
                    .HasColumnType("datetime")
                    .HasColumnName("PONCHESALIDA");

                entity.Property(e => e.Posicion)
                    .HasMaxLength(200)
                    .HasColumnName("POSICION");

                entity.Property(e => e.Posicionsupervisor)
                    .HasMaxLength(200)
                    .HasColumnName("POSICIONSUPERVISOR");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(100)
                    .HasColumnName("TELEFONO");

                entity.Property(e => e.Telefonosupervisor)
                    .HasMaxLength(20)
                    .HasColumnName("TELEFONOSUPERVISOR");

                entity.Property(e => e.Trabajahoy).HasColumnName("Trabajahoy");

                entity.Property(e => e.Vicepresidencia)
                    .HasMaxLength(200)
                    .HasColumnName("VICEPRESIDENCIA"); 

                entity.Property(e => e.RealHoraIni)
                    .HasColumnType("datetime")
                    .HasColumnName("REALHORAINI");

                entity.Property(e => e.OffPremise)
                    .HasColumnType("bit")
                    .HasColumnName("OffPremise");

                entity.HasOne(e => e.Ausentismo)
                    .WithMany(r => r.Horarios)
                    .HasForeignKey(e => e.Aucod)
                    .IsRequired(false);

            });

            modelBuilder.Entity<Parametro>(entity =>
            {
                entity.HasKey(e => e.RowId);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50);

                entity.Property(e => e.Empresa).HasMaxLength(50);

                entity.Property(e => e.Parametro1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Parametro");

                entity.Property(e => e.Valor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Mensaje)
                    .HasMaxLength(250);

                entity.Property(e => e.RowId)
                    .IsRequired();
            });

            modelBuilder.Entity<PosicionesOffPremise>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Posiciones_off_premise");

                entity.Property(e => e.Posicion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("POSICION");
            });

            modelBuilder.Entity<ParametroCorreos>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.EmpresaObject)
                .WithMany(r => r.ParametroCorreos)
                .HasForeignKey(e => e.Empresa);
                
            });

            modelBuilder.Entity<PosicionOffPremiseHeader>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .UseIdentityColumn();
                entity.HasOne(e => e.EmpresaObject)
                              .WithMany(r => r.PosicionOffPremises)
                              .HasForeignKey(e => e.Empresa);
            });

            modelBuilder.Entity<PosicionOffPremiseDetails>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .UseIdentityColumn();
                entity.HasOne(e => e.Header)
                .WithMany(r => r.Details)
                .HasForeignKey(e => e.IdHeader)
                .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<PosicionesFeriados>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Posiciones_Feriados");
                entity.Property(e => e.Id)
                     .UseIdentityColumn();
                entity.Property(e => e.Posicion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("POSICION");

            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
