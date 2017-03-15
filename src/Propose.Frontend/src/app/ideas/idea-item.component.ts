import { Idea } from "./idea.model";
import { IdeaService } from "./idea.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";
import { CurrentUser } from "../users";

const template = require("./idea-item.component.html");
const styles = require("./idea-item.component.scss");

export class IdeaItemComponent extends HTMLElement {
    constructor(
        private _ideaService: IdeaService = IdeaService.Instance,
        private _router: Router = Router.Instance,
        private _currentUser: CurrentUser = CurrentUser.Instance
    ) {
        super();

        this._onDeleteClick = this._onDeleteClick.bind(this);
        this._onEditClick = this._onEditClick.bind(this);
        this._onViewClick = this._onViewClick.bind(this);
        this._onClick = this._onClick.bind(this);
    }

    static get observedAttributes() {
        return ["entity"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    disconnectedCallback() {
        this._deleteLinkElement.removeEventListener("click", this._onDeleteClick);
        this._editLinkElement.removeEventListener("click", this._onEditClick);
        this._viewLinkElement.removeEventListener("click", this._onViewClick);
        this.removeEventListener("click", this._onClick);
    }

    private _bind() {
        this._nameElement.textContent = `${this.entity.name} - ${this.entity.votes.length}`;
        this._htmlBodyElement.innerHTML = this.entity.htmlBody;
    }

    private _setEventListeners() {
        this._deleteLinkElement.addEventListener("click", this._onDeleteClick);
        this._editLinkElement.addEventListener("click", this._onEditClick);
        this._viewLinkElement.addEventListener("click", this._onViewClick);
        this.addEventListener("click", this._onClick);
    }

    private async _onDeleteClick(e:Event) {
        await this._ideaService.remove({ id: this.entity.id });        
        this.parentNode.removeChild(this);
    }

    private _onEditClick() {
        this._router.navigate(["idea", "edit", this.entity.id]);
    }

    private _onViewClick() {
        this._router.navigate(["idea","view",this.entity.id]);
    }

    private async _onClick() {
        await this._ideaService.vote({ id: this.entity.id });
        this._router.navigate(["idea", "list"]);
    }
    
    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "entity":
                this.entity = JSON.parse(newValue);
                break;
        }        
    }

    private get _nameElement() { return this.querySelector("p") as HTMLElement; }
    private get _deleteLinkElement() { return this.querySelector(".entity-item-delete") as HTMLElement; }
    private get _editLinkElement() { return this.querySelector(".entity-item-edit") as HTMLElement; }
    private get _viewLinkElement() { return this.querySelector(".entity-item-view") as HTMLElement; }
    private get _htmlBodyElement() { return this.querySelector(".idea-html-body") as HTMLElement; }

    public entity: Idea;
}

customElements.define(`ce-idea-item`,IdeaItemComponent);
