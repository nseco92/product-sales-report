# Vueling-prueba-tecnica

## ARQUITECTURA
He seguido una arquitectura de N-capas con las siguientes capas:
	### Application
		- Lógica de coodinación entre capa presentación y dominio (ProductTransactionService, RateService, TransactionService) (Servicios más sus interfaces).
		- Dtos para transferir datos entre capas.
		- ProfileMapper responsable de como mapear objetos de dominio a DTO.
	### DistributedServices: 
		- Interactúa con servicios externos para obtener datos (tasas de cambio y transacciones).
		- Se definen interfaces que definen los contratos para los servicios (RateExternalService y TransactionExternalService).
	### Domain: 
		- Implementa la lógica de negocio principal.
		- Entidades.
		- IRateRepository y ITransactionRepository, interfaces que definen las operaciones necesarias para interactuar con los repositorios(recuperan y almacenan datos de entidades) de las entidades Rate y Transaction.
		- CurrencyConverterService se encarga de la lógica de negocio para convertir las transacciones en la moneda objetivo utilizando las tasas de cambio proporcionadas.
	### Infrastructure:
		- Proporciona acceso a datos externos, persistencia de datos y configuración de las entidades del modelo.
		- RateEntityConfiguration y TransactionEntityConfiguration definen cómo deben ser las entidades en el contexto de base de datos.
		- RateRepository y TransactionRepository responsables de acceder a los datos de las entidades y manejar el CRUD en base de datos, también interactúan con servicios externos para obtener y sincronizar datos. 
		- Archivos relacionados con EntitiFramework(migraciones, ApplicationDbContextModelSnapshot, configuración contexto base de datos).
	### Presentation:
		- Controladores para manejar las diferentes solucitudes del programa.
		- GlobalExceptionHandlingMiddleware, middleware para manejar excepciones globales en la aplicación. Registra las excepciones y devuelve una respuesta de error con el código de estado 500.
		- Logs de errores
	### Test: 
		- Pruebas Unitarias realizadas con xUnit.
		- Pruebas para los Controllers y para el CurrencyConverterService.

## BASE DE DATOS
Aunque la persistencia de datos se podría haber hecho también mediante caché, he optado por base de datos por ser más complejo para la prueba por una parte y más seguro y escalable por otro.

## NAMING CONVENTION
En general he usado PascalCase excepto para las variables (camelCase), parámetros (camelCase) y las constantes (mayúsculas). Además las variables internas tiene un guión bajo como prefijo.
Para la base de datos he seguido el ejemplo de NorthWind(PascalCase).

## UNIT TESTS
He utilizado el Framework xUnit, tengo tests tanto para los controllers como para el servicio principal CurrencyConverterService.

## CONTROL Y LOG DE ERRORES
He creado un middleware para gestionar los errores en el que se logea el error y se devuelve una error 500, además hay unos logs en los repositorios de la capa infraestructura para avisar si falla el servicio externo y coge los datos por base de datos.
Los logs se generan en la capa de presentación.

## PRINCIPIOS SOLID
	### Principio de responsabilidad única 
		- Se ha seguido al dividir la funcionalidad en clases específicas como RateService y TransactionService, cada una de las cuales tiene una única responsabilidad.
	### Principio de abierto/cerrado
		- Conseguido mediante el usa de interfaces como IRateService, ITransactionService y IProductTransactionService. Permiten que la funcionalidad se extienda o reemplace sin modificar las clases que dependen de ellas.
	### Principio de sustitución de Liskov
		- Aunque no he utilizado clases heredadas, el uso de interfaces y abstracciones promueve la capacidad de extender y reemplazar clases sin alterar las dependencias.
	### Principio de segregación de interfaces
		- En el caso de interfaces como IRateService y ITransactionService, están diseñadas para ser pequeñas y específicas, evitando que las clases que las implementan tengan que implementar métodos no relevantes para ellas.
	### Principio de inversión de dependencias
		- En la aplicación, este principio se aplica al inyectar dependencias a través de interfaces en lugar de depender directamente de las implementaciones de las clases. Por ejemplo ProductTransactionService depende de IRateService, ITransactionService y ICurrencyConverterService en lugar de sus implementaciones concretas.

## OTROS
Para obtener los datos de una api externa he añadido los ejemplos del documento más algunos más que me inventado a API Mocha, los links resultantes son los siguientes:
   - Rates: https://apimocha.com/productsalesreport/rates.json
   - Transactions: https://apimocha.com/productsalesreport/transactions.json