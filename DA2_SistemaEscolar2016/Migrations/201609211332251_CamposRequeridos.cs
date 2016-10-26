namespace DA2_SistemaEscolar2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposRequeridos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Alumnoes", "nombre", c => c.String(nullable: false));
            AlterColumn("dbo.Alumnoes", "apellidoPaterno", c => c.String(nullable: false));
            AlterColumn("dbo.Alumnoes", "apellidoMaterno", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Alumnoes", "apellidoMaterno", c => c.String());
            AlterColumn("dbo.Alumnoes", "apellidoPaterno", c => c.String());
            AlterColumn("dbo.Alumnoes", "nombre", c => c.String());
        }
    }
}
