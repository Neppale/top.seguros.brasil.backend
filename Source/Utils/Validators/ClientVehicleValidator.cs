static class ClientVehicleValidator
{
  /** <summary> Esta função verifica se o veículo escolhido pertence ao cliente escolhido. </summary>**/
  public static bool Validate(int id_cliente, int id_veiculo, SqlConnection connectionString)
  {

    var vehicle = GetVehicleByIdRepository.Get(id: id_veiculo, connectionString);
    if (vehicle == null) return false;

    if (vehicle.id_cliente != id_cliente) return false;

    return true;
  }
}