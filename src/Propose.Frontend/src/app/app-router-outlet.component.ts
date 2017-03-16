import { RouterOutlet, RouteReloadMiddleware } from "./router";
import { AuthorizedRouteMiddleware } from "./users";

export class AppRouterOutletComponent extends RouterOutlet {
    constructor(el: any) {
        super(el);
    }

    connectedCallback() {
        this.setRoutes([
            { path: "/", name: "idea-list", authRequired: true },
            { path: "/idea/edit/:ideaId", name: "idea-edit", authRequired: true },
            { path: "/idea/create", name: "idea-edit", authRequired: true },
            { path: "/idea/list", name: "idea-list", authRequired: true },
            { path: "/ideation/:ideationId/idea/list", name: "idea-list", authRequired: true },

            { path: "/idea/edit/:ideaId/tab/:tabIndex", name: "idea-edit", authRequired: true },

            { path: "/ideation/edit/:ideationId", name: "ideation-edit", authRequired: true },
            { path: "/ideation/create", name: "ideation-edit", authRequired: true },
            { path: "/ideation/list", name: "ideation-list", authRequired: true },

            { path: "/register", name: "register" },

            { path: "/change-password", name: "change-password", authRequired:true },
            { path: "/login", name: "login" },
            { path: "/error", name: "error" },
            { path: "*", name: "not-found" }

        ] as any);

        this.use(new AuthorizedRouteMiddleware());
        this.use(new RouteReloadMiddleware());

        super.connectedCallback();
    }
}

customElements.define(`ce-app-router-oulet`, AppRouterOutletComponent);