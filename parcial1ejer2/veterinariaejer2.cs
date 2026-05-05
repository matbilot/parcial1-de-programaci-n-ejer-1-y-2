using System;
using System.Collections.Generic;
using System.Linq;

namespace Veterinaria
{
    // =========================
    // CLASE ABSTRACTA ANIMAL
    // =========================
    public abstract class Animal
    {
        private string _nombre;
        private string _idAnimal;
        private string _categoria;

        public string Nombre => _nombre;
        public string IdAnimal => _idAnimal;

        public string Categoria
        {
            get => _categoria;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("La categoría no puede estar vacía.");
                _categoria = value;
            }
        }

        public int Anio { get; set; }

        public Animal(string id, string nombre, string categoria, int anio)
        {
            _idAnimal = id;
            _nombre = nombre;
            _categoria = string.IsNullOrEmpty(categoria) ? "Sin categoría" : categoria;
            Anio = anio;
        }

        public abstract decimal CalcularCostoBase();

        public virtual string ObtenerFicha()
        {
            return $"{IdAnimal} | {Nombre} | {Categoria} | Año: {Anio}";
        }
    }

    // =========================
    // INTERFAZ
    // =========================
    public interface IVacunable
    {
        bool EstaVacunado { get; }
        void AplicarVacuna(string detalle);
        void RegistrarRefuerzo();
        string ObtenerEstado();
    }

    // =========================
    // PERRO
    // =========================
    public class Perro : Animal, IVacunable
    {
        public string Raza { get; set; }
        public double PesoKg { get; set; }

        private bool _estaVacunado;
        public bool EstaVacunado => _estaVacunado;

        public Perro(string id, string nombre, string categoria, int anio,
                     string raza, double peso)
            : base(id, nombre, categoria, anio)
        {
            Raza = raza;
            PesoKg = peso;
        }

        public override decimal CalcularCostoBase()
        {
            return 3500 + (decimal)(PesoKg * 100);
        }

        public override string ObtenerFicha()
        {
            return base.ObtenerFicha() + $" | Raza: {Raza} | Peso: {PesoKg}kg";
        }

        public void AccionPropia()
        {
            Console.WriteLine($"{Nombre} está moviendo la cola");
        }

        public void AplicarVacuna(string detalle)
        {
            _estaVacunado = true;
        }

        public void RegistrarRefuerzo()
        {
            Console.WriteLine($"{Nombre} recibió refuerzo.");
        }

        public string ObtenerEstado()
        {
            return EstaVacunado ? "Al día" : "Pendiente";
        }
    }

    // =========================
    // GATO
    // =========================
    public class Gato : Animal
    {
        public bool EsCastrado { get; set; }
        public string ColorPelaje { get; set; }

        public Gato(string id, string nombre, string categoria, int anio,
                    string color, bool castrado)
            : base(id, nombre, categoria, anio)
        {
            ColorPelaje = color;
            EsCastrado = castrado;
        }

        public override decimal CalcularCostoBase()
        {
            return EsCastrado ? 2800m : 3200m;
        }

        public override string ObtenerFicha()
        {
            return base.ObtenerFicha() +
                   $" | Color: {ColorPelaje} | Castrado: {(EsCastrado ? "Sí" : "No")}";
        }
    }

    // =========================
    // CONSULTA
    // =========================
    public class Consulta
    {
        public string IdConsulta { get; set; }
        public string IdPaciente { get; set; }
        public string NombreVeterinario { get; set; }
        public string Motivo { get; set; }
        public decimal Costo { get; set; }
        public DateTime Fecha { get; set; }

        public Consulta(string idConsulta, string idPaciente, string nombreVeterinario,
                        string motivo, decimal costo, DateTime fecha)
        {
            IdConsulta = idConsulta;
            IdPaciente = idPaciente;
            NombreVeterinario = nombreVeterinario;
            Motivo = motivo;
            Costo = costo;
            Fecha = fecha;
        }
    }

    // =========================
    // RECURSIVIDAD
    // =========================
    public static class Utilidades
    {
        public static Animal? BuscarAnimalPorId(List<Animal> lista, string id, int indice = 0)
        {
            if (indice >= lista.Count)
                return null;

            if (lista[indice].IdAnimal == id)
                return lista[indice];

            return BuscarAnimalPorId(lista, id, indice + 1);
        }
    }

    // =========================
    // MAIN
    // =========================
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== PASO 1: CARGA DE REGISTROS ===");

            List<Animal> registros = new List<Animal>()
            {
                new Perro("P001","Rocky","Perro",4,"Labrador",32),
                new Perro("P002","Luna","Perra",2,"Beagle",10),
                new Gato("P003","Michi","Gato",6,"Gris",true),
                new Gato("P004","Simba","Gato",3,"Naranja",false),
                new Perro("P005","Rex","Perro",7,"Pastor Alemán",40),
                new Perro("P006","Manchita","Perra",1,"Dálmata",8)
            };

            foreach (var a in registros)
                Console.WriteLine(a.ObtenerFicha());

            Console.WriteLine("\n=== PASO 2: CARGA DE CONSULTAS ===");

            List<Consulta> registros2 = new List<Consulta>()
            {
                new Consulta("C001","P001","Dra. García","Control anual",4500,new DateTime(2026,4,1)),
                new Consulta("C002","P001","Dr. Pérez","Vacuna antirrábica",3200,new DateTime(2026,4,15)),
                new Consulta("C003","P002","Dra. García","Desparasitación",2800,new DateTime(2026,4,10)),
                new Consulta("C004","P003","Dr. Martínez","Revisión dental",6500,new DateTime(2026,3,20)),
                new Consulta("C005","P004","Dr. Martínez","Control de peso",2100,new DateTime(2026,4,5)),
                new Consulta("C006","P005","Dr. Pérez","Cirugía menor",12000,new DateTime(2026,4,22)),
                new Consulta("C007","P006","Dra. García","Primera consulta",3500,new DateTime(2026,4,25)),
                new Consulta("C008","P003","Dr. Pérez","Seguimiento post-op",4000,new DateTime(2026,2,18))
            };

            Console.WriteLine("\n=== PASO 3: AGREGAR ===");

            var thor = new Perro("P007","Thor","Perro",3,"Golden Retriever",28);
            registros.Add(thor);

            Console.WriteLine("✔ Thor agregado exitosamente.");
            Console.WriteLine(thor.ObtenerFicha());

            Console.WriteLine("\n=== PASO 4: ELIMINAR ===");

            var simba = registros.FirstOrDefault(x => x.Nombre == "Simba");
            if (simba != null)
            {
                registros.Remove(simba);
                Console.WriteLine("✔ Simba eliminado del sistema.");
            }

            var kira = registros.FirstOrDefault(x => x.Nombre == "Kira");
            if (kira == null)
                Console.WriteLine("✘ No se encontró ningún registro con el nombre Kira.");

            Console.WriteLine("\n=== PASO 5: POLIMORFISMO ===");

            foreach (var a in registros)
            {
                Console.WriteLine(a.ObtenerFicha());
                if (a is Perro p) p.AccionPropia();
            }

            Console.WriteLine("\n=== PASO 6: IVACUNABLE ===");

            var animalRocky = registros.FirstOrDefault(x => x.Nombre == "Rocky");

            if (animalRocky is IVacunable rocky)
            {
                rocky.AplicarVacuna("prueba");
                Console.WriteLine("✔ AplicarVacuna aplicado a Rocky.");

                rocky.RegistrarRefuerzo();
                Console.WriteLine("✔ RegistrarRefuerzo ejecutado para Rocky.");

                Console.WriteLine("Estado de Rocky: " + rocky.ObtenerEstado());
            }

            Console.WriteLine("\n=== CONSULTA 1 ===");
            foreach (var a in registros.OrderByDescending(x => x.Anio))
                Console.WriteLine($"{a.Nombre} — {a.Anio}");

            Console.WriteLine("\n=== CONSULTA 2 ===");
            var abril = registros2.Where(c =>
                c.NombreVeterinario == "Dra. García" &&
                c.Fecha.Month == 4 &&
                c.Fecha.Year == 2026);

            foreach (var c in abril)
                Console.WriteLine($"{c.Motivo} | ID: {c.IdConsulta} | Importe: ${c.Costo}");

            Console.WriteLine("\n=== CONSULTA 3 ===");
            var total = registros2.GroupBy(c => c.IdPaciente)
                                  .Select(g => new
                                  {
                                      Id = g.Key,
                                      Total = g.Sum(x => x.Costo)
                                  })
                                  .OrderByDescending(x => x.Total);

            foreach (var t in total)
            {
                var nombre = registros.FirstOrDefault(x => x.IdAnimal == t.Id)?.Nombre;
                Console.WriteLine($"{nombre}: ${t.Total}");
            }

            Console.WriteLine("\n=== CONSULTA 4 ===");

            Console.WriteLine("Total de pacientes registrados: " + registros.Count);
            Console.WriteLine("Cantidad de perros: " + registros.Count(x => x is Perro));
            Console.WriteLine("Cantidad de gatos: " + registros.Count(x => x is Gato));
            Console.WriteLine("Costo promedio de consultas: " + registros2.Average(x => x.Costo));
            Console.WriteLine("Consulta más cara: " + registros2.Max(x => x.Costo));

            // =========================
            // PROBLEMA 2
            // =========================

            Console.WriteLine("\n=== TAREA 1: HISTORIAL ROCKY ===");

            List<Consulta> historialRocky = new List<Consulta>();
            decimal totalRocky = 0;

            for (int i = 0; i < registros2.Count; i++)
            {
                if (registros2[i].IdPaciente == "P001")
                {
                    historialRocky.Add(registros2[i]);
                    totalRocky += registros2[i].Costo;
                }
            }

            foreach (var c in historialRocky)
            {
                Console.WriteLine($"{c.IdConsulta} | {c.Fecha:dd/MM/yyyy} | {c.Motivo} | Responsable: {c.NombreVeterinario} | ${c.Costo}");
            }

            Console.WriteLine($"Total acumulado de Rocky: ${totalRocky}");

            Console.WriteLine("\n=== TAREA 2: COSTOS BASE ===");

            int index = 0;
            while (index < registros.Count)
            {
                var a = registros[index];
                Console.WriteLine($"{a.Nombre} ({a.Categoria}) → Costo base: ${a.CalcularCostoBase()}");
                index++;
            }

            Console.WriteLine("\n=== REPORTE POR RESPONSABLE ===");

            string[] responsables = { "Dra. García", "Dr. Pérez", "Dr. Martínez" };

            int r = 0;
            decimal totalGeneral = 0;

            do
            {
                int cantidad = 0;
                decimal suma = 0;

                for (int i = 0; i < registros2.Count; i++)
                {
                    if (registros2[i].NombreVeterinario == responsables[r])
                    {
                        cantidad++;
                        suma += registros2[i].Costo;
                    }
                }

                Console.WriteLine($"{responsables[r]} → {cantidad} registros | Total: ${suma}");
                totalGeneral += suma;
                r++;

            } while (r < responsables.Length);

            Console.WriteLine("─────────────────────────────");
            Console.WriteLine($"TOTAL GENERAL: ${totalGeneral}");

            Console.WriteLine("\n=== EJERCICIO 1: RECURSIVIDAD ===");

            var encontrado = Utilidades.BuscarAnimalPorId(registros, "P005");
            Console.WriteLine(encontrado != null ? encontrado.ObtenerFicha() : "No encontrado");

            var noExiste = Utilidades.BuscarAnimalPorId(registros, "P999");
            if (noExiste == null)
                Console.WriteLine("P999 no encontrado.");

            // =========================
            // EJERCICIO 2 — ARRAY
            // =========================

            Console.WriteLine("\n=== EJERCICIO 2: COSTOS POR ANIMAL ===");

            decimal[] costosPorAnimal = new decimal[registros.Count];

            for (int i = 0; i < registros.Count; i++)
            {
                decimal suma = 0;

                for (int j = 0; j < registros2.Count; j++)
                {
                    if (registros[i].IdAnimal == registros2[j].IdPaciente)
                        suma += registros2[j].Costo;
                }

                costosPorAnimal[i] = suma;
                Console.WriteLine($"{registros[i].Nombre}: ${suma}");
            }

            decimal mayor = 0;
            decimal menor = decimal.MaxValue;
            string nombreMayor = "";
            string nombreMenor = "";

            decimal totalArr = 0;
            int contador = 0;

            for (int i = 0; i < costosPorAnimal.Length; i++)
            {
                if (costosPorAnimal[i] > 0)
                {
                    totalArr += costosPorAnimal[i];
                    contador++;

                    if (costosPorAnimal[i] > mayor)
                    {
                        mayor = costosPorAnimal[i];
                        nombreMayor = registros[i].Nombre;
                    }

                    if (costosPorAnimal[i] < menor)
                    {
                        menor = costosPorAnimal[i];
                        nombreMenor = registros[i].Nombre;
                    }
                }
            }

            Console.WriteLine($"Mayor gasto: {nombreMayor} — ${mayor}");
            Console.WriteLine($"Menor gasto: {nombreMenor} — ${menor}");
            Console.WriteLine($"Promedio: ${totalArr / contador}");

            // =========================
            // EJERCICIO 3 — MATRIZ
            // =========================

            Console.WriteLine("\n=== EJERCICIO 3: MATRIZ ===");

            string[] responsablesMatriz = { "Dra. García", "Dr. Pérez", "Dr. Martínez" };

            decimal[,] matriz = new decimal[registros.Count, responsablesMatriz.Length];

            for (int i = 0; i < registros.Count; i++)
            {
                for (int j = 0; j < responsablesMatriz.Length; j++)
                {
                    decimal suma = 0;

                    for (int k = 0; k < registros2.Count; k++)
                    {
                        if (registros[i].IdAnimal == registros2[k].IdPaciente &&
                            registros2[k].NombreVeterinario == responsablesMatriz[j])
                        {
                            suma += registros2[k].Costo;
                        }
                    }

                    matriz[i, j] = suma;
                }
            }

            Console.WriteLine("Paciente\tDra. García\tDr. Pérez\tDr. Martínez");

            for (int i = 0; i < registros.Count; i++)
            {
                Console.Write(registros[i].Nombre + "\t");

                for (int j = 0; j < responsablesMatriz.Length; j++)
                {
                    Console.Write($"${matriz[i, j]}\t");
                }

                Console.WriteLine();
            }

            decimal[] totales = new decimal[responsablesMatriz.Length];

            for (int j = 0; j < responsablesMatriz.Length; j++)
            {
                decimal suma = 0;

                for (int i = 0; i < registros.Count; i++)
                {
                    suma += matriz[i, j];
                }

                totales[j] = suma;
                Console.WriteLine($"{responsablesMatriz[j]} total: ${suma}");
            }

            decimal max = 0;
            string mejor = "";

            for (int i = 0; i < totales.Length; i++)
            {
                if (totales[i] > max)
                {
                    max = totales[i];
                    mejor = responsablesMatriz[i];
                }
            }

            Console.WriteLine($"Responsable con mayor recaudación: {mejor}");
        }
    }
}