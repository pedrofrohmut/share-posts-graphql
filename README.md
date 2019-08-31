# Share Posts

 A blog where the post contributions are shared with multiple developers to reach
a richer and more active posts feed.

### Backend

Will be used: EntityFrameworkCore, GraphQL and PostgreSQL

1. WebApi: a GraphQL Web Api with a single endpoint that resolves access to data
not directly, but through the DataAccess project.

2. DataAccess: The Repositories of this App. The interfaces and implementations 
in EFCore that access the DataBase project to execute data operations.

3. DataBase: Holds the Entities, The DbContext, DataSeeds and the Migrations of
this solution.

### FrontEnd

Will be used: React (TypeScript), Redux, GraphQL and BootstrapCSS

