using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    //misil,pais,frente
    public class Misil
    {
        //Clave Principal NO AUTONUMERICA
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdMisil { get; set; }
        public string Nombre { get; set; }
        public float Megatones { get; set; }
        //Escribe las propiedades de navegación a otras Entidades

        [System.Text.Json.Serialization.JsonIgnore]
        //public List<Misil> Misiles { get; } = new List<Misil>();
        //[System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Contrato> Contratos { get; set; }

        // A implementar
        // public override string ToString() => "A Implementar";
        public override string ToString() => $"#{IdMisil} Potencia de megatones:{Megatones}, y nombre de:{Nombre}";
    }
    public class Frente
    {
        //Clave Principal String
        [Key]
        public int IdFrente { get; set; }
        public string Pais { get; set; }

        //Escribe las propiedades de navegación a otras Entidades

        [System.Text.Json.Serialization.JsonIgnore]

        public List<Frente> Frentes { get; } = new List<Frente>();
         [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Contrato> Contratos { get; set; }

        // A implementar
        //public override string ToString() => $"A implementar";
        public override string ToString() => $"{IdFrente} Pais={Pais}";
    }
    
    public class Contrato
    {
        //Decide cómo vas a implementar la clave principal
        [Key]
        public int idContrato {get; set; }
        public int Cantidad { get; set; }
        
        //Escribe las propiedades de relación 1:N 
        //poner los id de las dos anteriores
        public int idFrente {get; set; }
        public int IdMisil { get; set; }
        
        //Escribe las propiedades de navegación a otras Entidades
        [System.Text.Json.Serialization.JsonIgnore]
        public Misil misil { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Frente frente { get; set; }

        // A implementar
        //public override string ToString() => $"A implementar";
        public override string ToString() => $"Este contrato tiene id {idContrato}, y es un contrato de {Cantidad} con misiles del tipo {misil.Nombre} firmado por {frente.Pais}.";
    }