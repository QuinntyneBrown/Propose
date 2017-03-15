import { Idea } from "./idea.model";
import { IdeaService } from "./idea.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";
import { IdeationService, Ideation } from "../ideations";

const template = require("./idea-edit.component.html");
const styles = require("./idea-edit.component.scss");

export class IdeaEditComponent extends HTMLElement {
    constructor(
        private _ideaService: IdeaService = IdeaService.Instance,
        private _ideationService: IdeationService = IdeationService.Instance,
        private _router: Router = Router.Instance
        ) {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onTitleClick = this.onTitleClick.bind(this);
    }

    static get observedAttributes() {
        return ["idea-id"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.ideaId ? "Edit Idea": "Create Idea";

        this.htmlBodyEditor = new EditorComponent(this._htmlBodyInputElement);

        this.htmlDescriptionEditor = new EditorComponent(this._htmlDescriptionInputElement);

        let promises:Array<Promise<any>> = [this._ideationService.get()];

        if (this.ideaId)
            promises.push(this._ideaService.getById(this.ideaId));

        const results = await Promise.all(promises);

        const ideations = results[0] as Array<Ideation>;
        
        for (let i = 0; i < ideations.length; i++) {
            let option = document.createElement("option");
            option.textContent = ideations[i].name;
            option.value = ideations[i].id;
            this._ideationSelectElement.appendChild(option);
        }

        if (this.ideaId) {
            const idea: Idea = results[1];                
            this._nameInputElement.value = idea.name;  
            this.htmlBodyEditor.setHTML(idea.htmlBody);
            this.htmlDescriptionEditor.setHTML(idea.htmlDescription);
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
        const idea = {
            id: this.ideaId,
            ideationId: this._ideationSelectElement.value,
            name: this._nameInputElement.value,
            htmlBody: this.htmlBodyEditor.text,
            htmlDescription: this.htmlDescriptionEditor.text
        } as Idea;
        
        await this._ideaService.add(idea);
        this._router.navigate(["idea","list"]);
    }

    public async onDelete() {        
        await this._ideaService.remove({ id: this.ideaId });
        this._router.navigate(["idea","list"]);
    }

    public onTitleClick() {
        this._router.navigate(["idea", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "idea-id":
                this.ideaId = newValue;
                break;
        }        
    }

    public ideaId: number;

    public htmlBodyEditor: EditorComponent;

    public htmlDescriptionEditor: EditorComponent;

    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".idea-name") as HTMLInputElement; }
    private get _htmlBodyInputElement(): HTMLDivElement { return this.querySelector(".idea-html-body") as HTMLDivElement; }
    private get _htmlDescriptionInputElement(): HTMLDivElement { return this.querySelector(".idea-html-description") as HTMLDivElement; }
    private get _ideationSelectElement(): HTMLSelectElement { return this.querySelector("select") as HTMLSelectElement; }
}

customElements.define(`ce-idea-edit`,IdeaEditComponent);
