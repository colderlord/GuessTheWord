import React, {Component} from 'react';
import {BrowserRouter, Route} from 'react-router-dom';
import {Layout} from './components/Layout';
import './custom.css';
import {StorageService} from "./services/storage.service";
import {SecurityService} from "./services/security.service";
import {ConfigurationService} from "./services/configuration.service";
import {GameInfoService} from "./services/gameInfo.service";
import {GuessWordService} from "./games/guessword/services/guessWord.service";
import {Home} from "./components/Home";
import RouteModel from "./models/route";
import {observer} from "mobx-react";
import {RoutingService} from "./services/routing.service";

const storageService = new StorageService();
const configurationService = new ConfigurationService(storageService);
const securityService = new SecurityService(configurationService, storageService);
export const guessWordService = new GuessWordService(configurationService, storageService);
const gameService = new GameInfoService([guessWordService]);
const routingService = new RoutingService([guessWordService]);

class App extends Component {
  static displayName = App.name;
  
  componentDidMount() {
      configurationService.load();
  }
  
  getGameInfos() {
      let routes : RouteModel[] = [
          {
              path: "/",
              element: Home
          }
      ];

      routes.push(...routingService.GetRoutes());

      return routes;
  }

  render() {
    return (
      <Layout gameInfoService={gameService}>
        <BrowserRouter>
          {this.getGameInfos().map((route, index) => {
              return <Route key={index} {...route} exact component={route.element} />;
          })}
        </BrowserRouter>
      </Layout>
    );
  }
}

export default observer(App)