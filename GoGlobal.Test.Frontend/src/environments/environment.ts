// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

import {Environment} from '../app/help/interfaces';

export const environment: Environment = {
  production: false,
  apiKeyFb: 'AIzaSyD5h4id7fR0qcC1U_dAlziDXPC_mdqcBqU',
  fbDbUrl: 'https://weather-cards-657ed-default-rtdb.firebaseio.com/',
  apiKey: '0ecc34d6d329fb1c62bcf0bf7778ebb1',
  apiUrl: 'http://localhost:5100',
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
