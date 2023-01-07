import * as React from "react";
import {RouteComponentProps} from "react-router";

export default class RouteModel {
    path?: string
    element?: React.ComponentType<RouteComponentProps<any>> | React.ComponentType<any> | undefined
}