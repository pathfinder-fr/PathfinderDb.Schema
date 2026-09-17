# Publication NuGet avec Trusted Publishing

La publication utilise GitHub Actions OIDC et ne nécessite pas de clé API
NuGet persistante. Le workflow `.github/workflows/publish.yml` demande un
jeton OIDC, puis `NuGet/login@v1` échange ce jeton contre une clé temporaire
valide uniquement pendant l'exécution.

## Configuration NuGet.org

Dans NuGet.org, ouvrir **Trusted Publishing** et créer une policy avec :

- propriétaire du dépôt : `pathfinder-fr` ;
- dépôt : `PathfinderDb.Schema` ;
- fichier workflow : `publish.yml` ;
- environnement GitHub : `nuget`.

Le nom du fichier est saisi sans le préfixe `.github/workflows/`.

## Configuration GitHub

L'environnement `nuget` doit autoriser les tags de release `v*.*.*`. Le
workflow possède la permission `id-token: write` et utilise la variable
d'environnement `NUGET_USER` pour le nom du compte NuGet, jamais l'adresse
email.

Configurer la variable d'environnement sans transmettre de secret au dépôt
local :

```powershell
gh variable set NUGET_USER `
  --repo pathfinder-fr/PathfinderDb.Schema `
  --env nuget `
  --body '<nom-du-compte-nuget>'
```

La valeur n'est pas une clé et peut aussi être définie depuis l'interface
**Settings > Environments > nuget > Environment variables**.

## Déclenchement

Après création et validation de la policy NuGet.org :

```powershell
git tag v2.0.0-preview.1
git push origin v2.0.0-preview.1
```

Le workflow construit, teste, valide et publie les fichiers `.nupkg` et
`.snupkg`, puis crée la GitHub Release associée.

## Vérifications de sécurité

- aucune variable `NUGET_API_KEY` persistante n'est requise ;
- la permission OIDC est limitée au job de publication ;
- l'environnement GitHub est limité aux tags de release ;
- la clé temporaire n'est utilisée que par l'étape de push.
