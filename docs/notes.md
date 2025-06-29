# 1. CQRS
## 🧠 ¿Por qué usar 2 bases de datos en CQRS?
### 🔹 Separar lectura y escritura te permite:
| Razón | Beneficio |
| ----- | --------- |
| Escalabilidad | Puedes escalar la base de lectura (read DB) de forma independiente — por ejemplo, con réplicas |
| Rendimiento | Puedes optimizar la base de lectura para queries rápidas (usando proyecciones, vistas materializadas, denormalización, etc.) |
| Desacoplamiento | Permite evolucionar el modelo de lectura sin afectar el dominio o los comandos |
| Tecnologías distintas | Puedes usar SQL para escritura y NoSQL (como Elasticsearch o Redis) para lectura |

## 🔄 ¿Cómo se sincronizan las 2 bases?
A través de eventos del dominio o integración, después de que ocurre un cambio de estado:
1. El comando modifica el estado del sistema (por ejemplo, se crea una `Photo` en la base de datos de escritura)
1. Se publica un evento como `PhotoCreated`
1. Un componente (listener, proyección, event handler) recibe ese evento y actualiza la base de lectura

## 🔧 ¿Dónde usar esto?
* En sistemas con mucha lectura (por ejemplo, catálogos, dashboards, APIs públicas)
* Si necesitas time-to-interactive muy bajo
* Si las consultas son muy distintas de la forma en que el dominio está estructurado
* Si quieres tener escalabilidad horizontal (por ejemplo, 10 réplicas de la base de lectura)

⚠️ ¿Cuándo NO vale la pena separar?
* En aplicaciones pequeñas o medianas
* Si el modelo de lectura y escritura son casi iguales
* Si no tienes problema de rendimiento



2. Patrón Unit of Work + Eventos
El repositorio o Unit of Work recolecta los eventos de las entidades persistidas.

Cuando se confirma la transacción, se publican automáticamente todos los eventos pendientes.

Si la persistencia falla, no se publica nada.

Esto asegura atomicidad.

Ejemplo (simplificado):

csharp
Copy
Edit
await _unitOfWork.CommitAsync(); // Guarda cambios y publica eventos
3. Uso de librerías/event sourcing
Frameworks como MediatR, EventFlow, o event sourcing libraries incorporan esta lógica.

Facilitan que los eventos se publiquen sólo si la transacción termina bien.


[Application Service / CommandHandler]
          │
          ├──> Crea entidad con evento
          ├──> Pasa entidad a repositorio
          └──> Repositorio/UnitOfWork guarda entidad y recopila eventos

[UnitOfWork Commit()]
          ├──> Persiste datos en DB
          └──> Publica eventos (ej. bus de mensajes)


1. Unit of Work + Evento Dispatcher externo
El repositorio sólo persiste.

El Unit of Work coordina la transacción y, después de persistir, notifica a un componente externo para que publique los eventos.

plaintext
Copy
Edit
Repositorio: solo guarda datos
UnitOfWork: coordina la transacción y entrega eventos a DomainEventDispatcher
DomainEventDispatcher: publica los eventos a la infraestructura
Aquí cada componente tiene su única responsabilidad.