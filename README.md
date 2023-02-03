# lmc4710-game-prototype

### Installation

After cloning the repo, open the project in Unity.
Additionally, run the following command in a terminal:

```
npm install
```

### Local Development

For local builds where the src build lives in Builds/Local, you can run the following command from VSCode to preview in browser

```
npm run dev-local
```

To run production builds locally, you can run:

```
npm run dev-production
```

### Building for production:

The builds folder contains a production build folder.
Replace this folder with a Unity WebGL buld when you want to release a new build to production.

If you want to store local builds on your machine, create a new folder named "Local" within the builds directory.
Store your local builds here, these builds will be ignored by git and won't be added to the remote repo.
