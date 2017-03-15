import { Idea } from "./idea.model";
import { IdeaService } from "./idea.service";
import { IdeaHub } from "./idea-hub.service";
import { IdeationService, Ideation } from "../ideations";
import { Router } from "../router";

const template = require("./idea-list.component.html");
const styles = require("./idea-list.component.scss");

export class IdeaListComponent extends HTMLElement {
    constructor(
        private _document: Document = document,
        private _ideaService: IdeaService = IdeaService.Instance,
        private _ideaHub: IdeaHub = IdeaHub.Instance,
        private _ideationService: IdeationService = IdeationService.Instance,
        private _router: Router = Router.Instance
    ) {
        super();
        this.onSelectChange = this.onSelectChange.bind(this);
        _ideaHub.subscribe(this._onOtherIdeaAddedOrUpdated);
    }

    private _onOtherIdeaAddedOrUpdated() {

    }

    private ideationId: any;

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private _setEventListeners() {
        this.selectElement.addEventListener("change", this.onSelectChange);
    }

    private async _bind() {
        const promises = [
            this._ideaService.get(),
            this._ideationService.get()
        ];

        const results: Array<any> = await Promise.all(promises);
        const ideations = results[1];
        const ideas = results[0];

        for (let i = 0; i < ideations.length; i++) {
            let option = document.createElement("option");
            option.textContent = `${ideations[i].name}`;
            option.value = ideations[i].id;
            this.selectElement.appendChild(option);
        }

        this.ideationId = ideations[0].id;

        if (this._router.activatedRoute.routeParams && this._router.activatedRoute.routeParams.ideationId)
            this.ideationId = this._router.activatedRoute.routeParams.ideationId;

        this.selectElement.value = this.ideationId;

        for (var i = 0; i < ideas.length; i++) {
            if ( ideas[i].ideationId == this.ideationId) {
                let el = this._document.createElement(`ce-idea-item`);
                el.setAttribute("entity", JSON.stringify(ideas[i]));
                this.cardsElement.appendChild(el);
            }
        }    
    }

    public onSelectChange() {
        this._router.navigate(["ideation", this.selectElement.value, "idea", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "ideation-id":
                this.ideationId = JSON.parse(newValue);

                if (this.parentNode) {
                    this.connectedCallback();
                }
                break;
        }
    }

    private get selectElement(): HTMLSelectElement { return this.querySelector("select") as HTMLSelectElement; }
    private get cardsElement(): HTMLElement { return this.querySelector(".idea-cards") as HTMLElement; }
}

customElements.define("ce-idea-list", IdeaListComponent);
