import { Ideation } from "./ideation.model";
import { IdeationService } from "./ideation.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";

const template = require("./ideation-edit.component.html");
const styles = require("./ideation-edit.component.scss");

export class IdeationEditComponent extends HTMLElement {
    constructor(
        private _ideationService: IdeationService = IdeationService.Instance,
        private _router: Router = Router.Instance
        ) {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onTitleClick = this.onTitleClick.bind(this);
    }

    static get observedAttributes() {
        return ["ideation-id"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.ideationId ? "Edit Ideation": "Create Ideation";

        if (this.ideationId) {
            const ideation: Ideation = await this._ideationService.getById(this.ideationId);                
            this._nameInputElement.value = ideation.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._titleElement.addEventListener("click", this.onTitleClick);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._titleElement.removeEventListener("click", this.onTitleClick);
    }

    public async onSave() {
        const ideation = {
            id: this.ideationId,
            name: this._nameInputElement.value
        } as Ideation;
        
        await this._ideationService.add(ideation);
        this._router.navigate(["ideation","list"]);
    }

    public async onDelete() {        
        await this._ideationService.remove({ id: this.ideationId });
        this._router.navigate(["ideation","list"]);
    }

    public onTitleClick() {
        this._router.navigate(["ideation", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "ideation-id":
                this.ideationId = newValue;
                break;
        }        
    }

    public ideationId: number;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".ideation-name") as HTMLInputElement;}
}

customElements.define(`ce-ideation-edit`,IdeationEditComponent);
