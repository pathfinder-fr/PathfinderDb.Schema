# PathfinderDb.Schema

`PathfinderDb.Schema` contient le modèle de données partagé par les exports Pathfinder-fr et leurs consommateurs. Il définit les objets sérialisés en XML et JSON pour les sorts, dons, monstres, sources, références, localisations et autres éléments du modèle Pathfinder Database.

Le dépôt est volontairement indépendant de `pf1-tools`. Le parser consomme cette librairie comme un package NuGet et produit des fichiers conformes au schéma défini ici. Le dépôt historique `WikiExportParser-Decompile` contient une copie de référence du code et des sorties produites en 2019, mais ce dépôt est désormais la base de travail pour les prochaines versions.

La version `2.0.0-preview.1` est publiée sur
[NuGet.org](https://www.nuget.org/packages/PathfinderDb.Schema/2.0.0-preview.1).
La [GitHub Release correspondante](https://github.com/pathfinder-fr/PathfinderDb.Schema/releases/tag/v2.0.0-preview.1)
est générée par GitHub Actions via Trusted Publishing OIDC.

## Contenu du dépôt

```text
src/PathfinderDb.Schema/         Modèle C# et attributs de sérialisation
tests/PathfinderDb.Schema.Tests/ Tests et fixtures de compatibilité
xml/                              Schémas XSD et exemples XML
docs/                             Documentation du projet
docs/reference/                   Références historiques du modèle
build/                            Scripts de validation et outils de build
```

Les fichiers XSD de `xml/` décrivent le contrat XML public. Les exemples associés servent de fixtures de compatibilité et doivent rester valides après chaque évolution du modèle.

## Contrat de données actuel

Le modèle principal est `DataSet`, qui peut contenir :

- les sources éditoriales ;
- les sorts et leurs niveaux par liste de classe ;
- les dons et leurs prérequis ;
- les monstres ;
- les en-têtes et informations de langue.

Les éléments partagent la gestion des sources, références et localisations via `Element`. Le format XML utilise actuellement le namespace `urn:pathfinderDb`.

### Référentiel anglais

Les données Pathfinder 1e en anglais peuvent être référencées avec
`Source.Ids.ArchivesOfNethys` (`aonprd`). La source correspond au site
[Archives of Nethys](https://aonprd.com/), qui héberge les données ouvertes de
référence. La librairie fournit uniquement l'identifiant de source et ne
redistribue pas le contenu du site.

Les sorties historiques de référence sont disponibles dans le dépôt frère :

```text
D:\code\perso\pf\WikiExportParser-Decompile\Out
```

Elles comprennent des datasets agrégés et des datasets séparés par source, en XML, JSON et CSV. La compatibilité avec l'ancien format est utile pour comparer les résultats, mais il n'existe qu'un seul utilisateur de cette librairie : les évolutions cassantes sont donc acceptables lorsqu'elles simplifient suffisamment le modèle ou la publication.

## Utilisation dans le pipeline Pathfinder-fr

Le dépôt `pf1-tools` référence actuellement le package NuGet `PathfinderDb.Schema`. Le parser `wiki-export-parser` utilise les types de cette librairie pour construire les datasets puis les sérialiser vers `pf1-data`.

Après publication d'une nouvelle version :

1. publier le package NuGet depuis ce dépôt ;
2. mettre à jour la version centralisée dans `pf1-tools/Directory.Packages.props` ;
3. reconstruire `wiki-export-parser` ;
4. comparer les sorties XML/JSON avec les fixtures et les exports historiques ;
5. relancer le diagnostic du corpus complet avant toute évolution du site ou des données publiées.

## Direction technique

La prochaine version doit privilégier un projet SDK moderne et un pipeline `dotnet restore`, `dotnet build`, `dotnet test` et `dotnet pack`. Le vieux profil .NET Portable, le projet MSBuild personnalisé et le `nuget.exe` embarqué ne sont pas des contraintes à préserver.

## Développement et package

```powershell
dotnet restore .\PathfinderDb.Schema.sln
dotnet build .\PathfinderDb.Schema.sln -c Release --no-restore
dotnet test .\PathfinderDb.Schema.sln -c Release --no-build
dotnet pack .\src\PathfinderDb.Schema\PathfinderDb.Schema.csproj `
  -c Release --no-build -o .\artifacts `
  -p:PackageVersion=2.0.0-preview.1
pwsh .\build\Validate-Package.ps1 `
  -PackagePath .\artifacts\PathfinderDb.Schema.2.0.0-preview.1.nupkg
```

Les artefacts générés sont limités à `.\artifacts\` à la racine du projet.
Le dossier `build/` contient uniquement les scripts et outils de construction.
Le package inclut le README, la licence, l'icône, les schémas XSD et les
exemples XML ainsi que le changelog de migration.
La publication NuGet sera effectuée par GitHub Actions à partir d'un tag
SemVer, après validation de cette même séquence. Elle utilise Trusted
Publishing avec OIDC et ne stocke pas de clé API NuGet persistante.

La version cible pourra modifier le contrat de manière cassante si cela permet :

- de supprimer les artefacts et outils obsolètes ;
- de simplifier le ciblage framework ;
- de rendre la publication reproductible ;
- de mieux représenter les listes de sorts modernes ;
- de rendre les tests de sérialisation fiables ;
- de documenter explicitement la version du schéma.

Voir `TODO.md` pour la feuille de route de cette migration.
Voir `docs/trusted-publishing.md` pour configurer la policy NuGet.org et
l'environnement GitHub de publication.
