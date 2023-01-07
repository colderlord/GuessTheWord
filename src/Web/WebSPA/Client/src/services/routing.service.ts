import RouteModel from "../models/route";

export class RoutingService {

    private routingServices: IRoutingService[];
    private readonly routes: RouteModel[];

    constructor(routingServices: IRoutingService[]) {
        this.routingServices = routingServices;
        this.routes = [];

        routingServices.forEach(rs => {
            this.routes.push(...rs.routes());
        });
    }

    GetRoutes() : RouteModel[] {
        return this.routes;
    }
}

export interface IRoutingService {
    routes(): RouteModel[]
}