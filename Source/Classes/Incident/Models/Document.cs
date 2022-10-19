class DocumentDto
{
    public string documento { get; set; }
    public string tipoDocumento { get; set; }

    public DocumentDto(string documento, string tipoDocumento)
    {
        this.documento = documento;
        this.tipoDocumento = tipoDocumento;
    }

    public DocumentDto()
    {
        // Default constructor
        this.documento = "any_document";
        this.tipoDocumento = "any_document_type";
    }
}