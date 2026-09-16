# Feuille de route — PathfinderDb.Schema

Objectif global : remettre `PathfinderDb.Schema` en état de maintenance active,
moderniser sa construction et rétablir une publication NuGet reproductible vers
[`PathfinderDb.Schema`](https://www.nuget.org/packages/PathfinderDb.Schema), tout
en conservant la compatibilité XML nécessaire à `pf1-tools`.

Le dépôt est une bibliothèque de contrat partagée. La migration technique doit
être séparée des évolutions du modèle métier afin de pouvoir identifier
précisément toute régression dans `pf1-tools` ou dans les exports historiques.

## État de référence connu

- [x] Identifier la dernière version NuGet : `1.0.3`, publiée le 20 novembre 2016.
- [x] Identifier le contenu historique du package : `lib/portable-net40+sl5+win`,
  DLL, PDB, XML de documentation et nuspec.
- [x] Vérifier que GitHub ne contient actuellement aucun workflow Actions ni
  release publiée.
- [x] Vérifier que le dépôt est resté pratiquement inchangé depuis mars 2019.
- [x] Vérifier que `pf1-tools` consomme actuellement `PathfinderDb.Schema` en
  version `1.0.3` via `Directory.Packages.props`.
- [x] Vérifier que le parser consommateur cible actuellement .NET Framework 4.8.
- [x] Vérifier que le build historique ne fonctionne plus avec le SDK .NET
  actuel : les targets Portable et les références .NET Framework 4.0 sont
  absentes.
- [x] Confirmer que la licence du dépôt est Apache-2.0.
- [x] Confirmer que le namespace XML public historique est `urn:pathfinderDb`.
- [x] Identifier l'issue GitHub encore ouverte : gestion des types de cible de
  sort.
- [x] Noter dans la documentation de migration les différences intentionnelles
  entre la version historique et la première version moderne.

## Principes de migration

- [ ] Ne pas modifier le namespace XML `urn:pathfinderDb` pendant la migration
  technique.
- [ ] Ne pas mélanger la conversion SDK-style, la nouvelle pipeline et une
  refonte importante du modèle dans une même étape non testée.
- [ ] Préserver les noms XML, les noms d'attributs, les enums sérialisés et les
  structures présentes dans les exports historiques.
- [ ] Considérer `WikiExportParser-Decompile` comme une référence
  comportementale et d'API, pas comme une source à comparer textuellement :
  son code est décompilé.
- [ ] Documenter les ruptures publiques éventuelles avant de les implémenter.
- [ ] Utiliser SemVer et réserver une version majeure aux changements de contrat
  ou de compatibilité réellement assumés.

## Jalons

| Jalon | Objectif | Sortie attendue |
|---|---|---|
| M0 | État de référence | Fixtures, API et compatibilité historiques identifiées |
| M1 | Projet moderne compilable | Bibliothèque et tests SDK-style construisibles |
| M2 | Contrat et tests stabilisés | Sérialisation XML/JSON couverte et comportement documenté |
| M3 | Package NuGet validé | Package complet, inspecté automatiquement et installable |
| M4 | CI de validation | Pull requests et branche principale protégées par build/tests |
| M5 | Préversion intégrée | `2.0.0-preview.1` consommée par `pf1-tools` sans référence locale |
| M6 | Publication stable | `2.0.0` publiée sur NuGet via un tag Git |
| M7 | Évolutions métier | Listes de sorts, métadonnées et contrat enrichi sur une base stable |

---

## M0 — Établir et figer l’état de référence

### Sources et fixtures

- [x] Conserver les exemples représentatifs de `xml/samples` :
  - `DataSetHeader.xml`
  - `Feat.xml`
  - `Monster.xml`
  - `Spell.xml`
- [x] Référencer des exports historiques représentatifs de
  `WikiExportParser-Decompile\Out` :
  - dataset de sorts ;
  - dataset de dons ;
  - dataset de monstres ;
  - au moins un dataset séparé par source.
- [x] Vérifier que les exports utilisent tous `urn:pathfinderDb`.
- [x] Relever les éléments, attributs, enums et collections réellement présents
  dans les exports.
- [x] Ajouter des fixtures minimales versionnées dans ce dépôt et un manifeste
  machine-readable plutôt que de dépendre uniquement d'un chemin local vers un
  dépôt frère.

### API et consommateur

- [x] Établir la liste des types et membres publics consommés par `pf1-tools`.
- [x] Relever les appels à `DataSet.Load`, `DataSet.Add`, aux constantes
  `Source.Ids`, `SpellList.Ids` et aux enums de sérialisation.
- [x] Identifier les sorties XML, JSON et CSV de référence produites par
  `wiki-export-parser`.
- [x] Documenter la version actuellement restaurée par `pf1-tools` :
  `PathfinderDb.Schema` `1.0.3`.
- [x] Documenter les différences entre la source actuelle, le package `1.0.3`
  et le code décompilé historique.
- [ ] Fermer ou traiter explicitement l'issue GitHub sur les types de cible de
  sort avant de déclarer le contrat métier stabilisé.

### Critères de sortie M0

- [x] Les fixtures minimales sont stockées dans le dépôt.
- [x] Le contrat XML historique est décrit.
- [x] La surface publique consommée par `pf1-tools` est connue.
- [ ] Une comparaison automatique des sorties historiques est possible.

---

## M1 — Moderniser les projets et rétablir le build

### Bibliothèque

- [x] Remplacer le projet Portable historique par un projet SDK-style.
- [x] Cibler `netstandard2.0` comme framework unique de la bibliothèque.
- [ ] Vérifier la compatibilité avec le parser .NET Framework 4.8.
- [x] Supprimer les références explicites aux profils Portable.
- [x] Supprimer les imports MSBuild Visual Studio 2010/2015.
- [x] Déplacer les métadonnées d'assembly dans le `.csproj`.
- [x] Supprimer ou désactiver les valeurs historiques incohérentes de
  `AssemblyVersion` et `AssemblyFileVersion`.
- [x] Préserver le namespace `PathfinderDb.Schema`.
- [ ] Préserver les attributs `XmlType`, `XmlRoot`, `XmlElement`, `XmlArray` et
  `XmlAttribute` existants tant qu'une rupture n'est pas documentée.
- [ ] Activer les nullable annotations uniquement après avoir évalué leur impact
  sur les consommateurs et la sérialisation.

### Projet de tests

- [x] Remplacer le projet MSTest historique par un projet SDK-style.
- [x] Utiliser un framework de test moderne et `Microsoft.NET.Test.Sdk`.
- [x] Cibler un framework SDK stable pour l'exécution des tests, par exemple
  `net8.0`.
- [x] Référencer la bibliothèque par `ProjectReference` pendant les tests
  unitaires.
- [x] Préparer des tests qui pourront ensuite référencer le package produit.

### Critères de sortie M1

- [x] `dotnet restore` fonctionne sur une machine propre.
- [x] `dotnet build -c Release` fonctionne sans Visual Studio historique.
- [x] `dotnet test -c Release` fonctionne avec le SDK installé sur l'agent CI.
- [ ] Le parser .NET Framework 4.8 peut référencer une build `netstandard2.0`.

---

## M2 — Stabiliser le contrat et les tests

### Sérialisation XML

- [x] Ajouter des tests XML aller-retour pour `DataSet`.
- [ ] Ajouter des tests pour `Spell`, `Feat`, `Monster`, `Source` et
  `ElementSource`.
- [ ] Tester les propriétés nulles, vides et absentes.
- [ ] Tester les sources uniques et multiples.
- [ ] Tester les références et localisations multiples.
- [ ] Tester les enums sérialisés sous forme d'attributs et les valeurs combinées
  de descripteurs.
- [ ] Vérifier que les anciennes fixtures XML sont toujours désérialisables.
- [x] Vérifier que les sorties XML conservent `urn:pathfinderDb`.
- [ ] Ajouter une validation XSD des documents produits lorsque cela est
  applicable.
- [x] Ajouter des tests de désérialisation des extraits historiques compacts
  pour les sorts, dons et monstres.

### Sérialisation JSON

- [x] Identifier et documenter le sérialiseur JSON utilisé par `pf1-tools`.
- [x] Ajouter des tests de sérialisation et désérialisation JSON.
- [x] Vérifier les noms de propriétés attendus par les consommateurs.
- [ ] Vérifier le comportement des propriétés nulles et des collections vides.
- [ ] Ajouter au moins un test sur une sortie JSON historique de sorts, dons et
  monstres.

### Compatibilité des données

- [ ] Ajouter un test de désérialisation d'un export historique complet de
  sorts.
- [ ] Ajouter un test de désérialisation d'un export historique complet de dons.
- [ ] Ajouter un test de désérialisation d'un export historique de monstres.
- [ ] Comparer les sorties normalisées avec les fixtures historiques.
- [ ] Documenter les différences acceptées, notamment celles qui proviennent
  uniquement du formatage ou de l'ordre des propriétés.

### Critères de sortie M2

- [ ] Les exemples du dépôt passent en désérialisation.
- [ ] Les fixtures historiques passent les tests de compatibilité.
- [ ] Toute différence de sortie est classée comme régression ou différence
  intentionnelle documentée.
- [ ] Le contrat XML public est couvert par des tests automatisés.

---

## M3 — Construire et valider le package NuGet

### Métadonnées NuGet

- [x] Définir dans le projet :
  - `PackageId` = `PathfinderDb.Schema`
  - auteurs et propriétaires ;
  - description ;
  - `PackageLicenseExpression` = `Apache-2.0` ;
  - `RepositoryUrl` ;
  - `PackageTags` ;
  - `PackageReadmeFile`.
- [x] Définir une icône de package embarquée dans le `.nupkg`.
- [x] Ajouter le README et `LICENSE.txt` au package.
- [x] Générer le fichier XML de documentation.
- [x] Générer les symboles au format `.snupkg`.
- [x] Désactiver `GeneratePackageOnBuild` par défaut.
- [x] Fournir une commande documentée pour produire localement un package.
- [x] Ne pas coder la version définitive en dur dans le pipeline.

### Contenu du package

- [x] Inclure `lib/netstandard2.0/PathfinderDb.Schema.dll`.
- [x] Inclure le XML de documentation associé.
- [x] Inclure les XSD utiles au contrat XML.
- [x] Inclure les exemples XML pertinents.
- [x] Inclure la licence et le README.
- [x] Vérifier qu'aucun fichier temporaire, PDB non souhaité ou chemin local
  n'est embarqué.

### Validation automatique

- [x] Ajouter une validation qui inspecte le contenu du `.nupkg`.
- [x] Vérifier le framework `netstandard2.0`.
- [x] Vérifier la présence du README, de la licence, du XML et des XSD.
- [x] Vérifier l'identifiant et la version du package.
- [x] Vérifier que le package est installable dans un projet de test externe.
- [x] Tester une restauration via NuGet plutôt qu'uniquement via
  `ProjectReference`.

### Nettoyage des outils historiques

- [x] Supprimer `build/build.proj` après validation du nouveau pipeline.
- [x] Supprimer `build.cmd` après validation de la commande locale moderne.
- [x] Supprimer `build/nuget.exe`.
- [x] Supprimer le nuspec manuel si le `.csproj` couvre entièrement les
  métadonnées.
- [ ] Mettre à jour `.gitignore` pour les sorties `artifacts/`, `bin/`, `obj/`
  et packages.

### Critères de sortie M3

- [ ] `dotnet pack -c Release` produit un `.nupkg` et un `.snupkg` valides.
- [ ] Le package contient tous les fichiers attendus.
- [ ] Le package peut être consommé par un projet `net48`.
- [ ] Le package peut être consommé par un projet moderne.

---

## M4 — Mettre en place la CI GitHub Actions

### Workflow de validation

- [x] Ajouter un workflow déclenché sur pull request.
- [x] Ajouter un workflow déclenché sur push de la branche principale.
- [x] Utiliser une version SDK .NET stable et explicitement supportée.
- [x] Exécuter `dotnet restore`.
- [x] Exécuter `dotnet build -c Release --no-restore`.
- [x] Exécuter `dotnet test -c Release --no-build`.
- [x] Exécuter `dotnet pack -c Release --no-build` avec une version CI.
- [x] Inspecter automatiquement le contenu du package.
- [x] Publier le package de validation comme artifact de workflow.
- [x] Configurer la concurrence pour éviter deux publications simultanées.

### Workflow de publication

- [x] Déclencher la publication uniquement sur les tags SemVer.
- [x] Accepter les tags de préversion, par exemple `v2.0.0-preview.1`.
- [x] Accepter les tags stables, par exemple `v2.0.0`.
- [x] Extraire la version depuis le tag après validation stricte.
- [x] Vérifier que la version du package correspond au tag.
- [x] Rejouer restore, build, tests et pack dans le job de publication.
- [x] Publier `.nupkg` et `.snupkg` sur NuGet.org.
- [x] Utiliser `https://api.nuget.org/v3/index.json` comme source.
- [x] Utiliser un secret GitHub `NUGET_API_KEY`.
- [x] Ne jamais afficher la clé dans les logs ou les scripts.
- [x] Utiliser `--skip-duplicate` pour rendre le job rejouable.
- [x] Utiliser un environment GitHub protégé pour la publication stable.
- [x] Conserver les packages publiés comme artifacts.
- [x] Créer ou mettre à jour une GitHub Release associée au tag.

### Critères de sortie M4

- [ ] Une pull request exécute build, tests, pack et validation du package.
- [ ] Un tag de test produit une préversion sans publication accidentelle sur
  la branche principale.
- [ ] Un tag stable publie exactement la version attendue.
- [ ] Une erreur de test ou de validation bloque la publication.
- [ ] Aucun secret n'apparaît dans les logs.

---

## M5 — Publier et intégrer la préversion dans `pf1-tools`

### Préversion

- [ ] Publier `2.0.0-preview.1`.
- [ ] Vérifier que la version apparaît sur NuGet.org.
- [ ] Vérifier le contenu et les frameworks du package distant.
- [ ] Vérifier qu'une restauration distante fonctionne sans source locale.

### Intégration `pf1-tools`

- [ ] Mettre à jour `pf1-tools/Directory.Packages.props` vers
  `2.0.0-preview.1`.
- [ ] Supprimer toute référence ou dépendance implicite à une DLL locale.
- [ ] Construire `wiki-export-parser` avec le package NuGet restauré.
- [ ] Exécuter les commandes :
  - `spells` ;
  - `feats` ;
  - `monsters`.
- [ ] Vérifier que les sorties XML sont lisibles par les consommateurs
  existants.
- [ ] Vérifier que les sorties JSON sont lisibles par les consommateurs
  existants.
- [ ] Vérifier que `pf1-data` conserve la structure attendue.
- [ ] Comparer les sorties avec les exports historiques de référence.
- [ ] Relancer le diagnostic du corpus complet.
- [ ] Documenter toute différence intentionnelle dans le changelog.

### Critères de sortie M5

- [ ] `pf1-tools` compile sans `ProjectReference` local vers ce dépôt.
- [ ] Les commandes de génération principales s'exécutent.
- [ ] Les sorties produites ne présentent aucune régression non expliquée.
- [ ] Les diagnostics du corpus sont acceptables et documentés.

---

## M6 — Publier la version stable

- [ ] Corriger les problèmes trouvés lors de l'intégration de la préversion.
- [ ] Publier éventuellement une nouvelle préversion si le contrat change.
- [ ] Valider le changelog de migration.
- [ ] Publier le tag `v2.0.0`.
- [ ] Vérifier la présence de `2.0.0` et de ses symboles sur NuGet.org.
- [ ] Mettre à jour `pf1-tools/Directory.Packages.props` vers `2.0.0`.
- [ ] Reconstruire `wiki-export-parser` uniquement avec le package stable.
- [ ] Relancer les tests et le diagnostic du corpus complet.
- [ ] Créer la GitHub Release stable avec les notes de migration.
- [ ] Documenter la procédure de publication et de restauration.

### Critères de sortie M6

- [ ] La version stable est disponible sur NuGet.org.
- [ ] `pf1-tools` utilise la version stable.
- [ ] Le parser et les sorties Pathfinder-fr sont validés.
- [ ] La procédure de publication est reproductible par un mainteneur.

---

## M7 — Évolutions du modèle métier

Ces travaux ne doivent commencer qu'après la publication stable et la validation
de `pf1-tools`.

### Dataset et métadonnées

- [ ] Décider si `DataSet` reçoit une propriété sérialisée de version de schéma.
- [ ] Évaluer les métadonnées de génération :
  - version du schéma ;
  - version du parser ;
  - date de génération ;
  - révision du corpus.
- [ ] Vérifier l'impact XML, JSON et XSD de chaque métadonnée.

### Listes de sorts

- [ ] Enrichir `SpellList` avec un nom d'affichage si nécessaire.
- [ ] Formaliser les identifiants des listes modernes :
  - chaman ;
  - hypnotiseur ;
  - médium ;
  - occultiste ;
  - psychiste ;
  - spirite ;
  - sanguin ;
  - autres listes réellement présentes dans le corpus.
- [ ] Décider si les identifiants restent des chaînes libres ou deviennent des
  objets `SpellList` référencés.
- [ ] Ajouter des fixtures et tests pour chaque nouvel identifiant.
- [ ] Traiter l'issue GitHub sur les types de cible de sort.

### Corrections de contrat

- [ ] Identifier les identifiants ou libellés historiquement mal orthographiés.
- [ ] Décider pour chaque correction entre compatibilité, alias ou rupture.
- [ ] Mettre à jour les XSD concernés.
- [ ] Ajouter des tests de lecture des anciennes valeurs.
- [ ] Documenter les migrations nécessaires dans `pf1-tools`.

### Monstres

- [ ] Décider si `Monster` reste limité aux champs d'index.
- [ ] Si le bloc de statistiques complet est ajouté, définir une extension
  progressive et testée.
- [ ] Ne pas modifier la structure existante sans fixture de compatibilité.

### Versionnement des évolutions métier

- [ ] Utiliser une version mineure pour les ajouts compatibles.
- [ ] Utiliser une version majeure pour les suppressions, renommages ou ruptures
  XML/JSON.
- [ ] Mettre à jour les XSD, exemples, README, changelog et tests à chaque
  évolution du contrat.

---

## Documentation à maintenir

- [ ] Mettre à jour le README avec :
  - la cible framework ;
  - la commande de build ;
  - la commande de test ;
  - la commande de pack ;
  - la procédure de publication ;
  - la procédure d'intégration dans `pf1-tools`.
- [ ] Ajouter un changelog de migration `1.0.3` vers `2.0.0`.
- [ ] Documenter le contrat XML et le namespace `urn:pathfinderDb`.
- [ ] Documenter les fichiers XSD et les exemples.
- [ ] Documenter la politique de versionnement.
- [ ] Documenter la procédure de validation des sorties historiques.
- [ ] Documenter les secrets et permissions nécessaires à GitHub Actions sans
  jamais inclure de valeur sensible.

## Définition de terminé

La remise en service est terminée uniquement lorsque :

- [ ] le projet se construit avec le SDK .NET moderne ;
- [ ] les tests de contrat XML/JSON passent ;
- [ ] le package est inspecté automatiquement ;
- [ ] une préversion a été consommée par `pf1-tools` ;
- [ ] `wiki-export-parser` fonctionne sans référence locale ;
- [ ] les sorties historiques et actuelles ont été comparées ;
- [ ] la version stable est publiée sur NuGet.org ;
- [ ] `pf1-tools` utilise la version stable ;
- [ ] la pipeline GitHub Actions est reproductible et documentée ;
- [ ] les outils de build historiques ont été supprimés ;
- [ ] les prochaines évolutions du modèle sont séparées de la migration
  technique et couvertes par des tests.
