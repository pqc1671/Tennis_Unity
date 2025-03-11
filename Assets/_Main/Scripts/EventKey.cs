
public enum EventKey
{
    OnRemoveFromEquipped,
    OnAddToEquipped,
    OnUpdateToEquipped,
    OnEquipDeficiency,
    OnEquipExcess,
    OnEquipEnough,
    MoveOnP8,
    TakeOffP10,
    StartChecktakeOff,
    ForceSetTransformItem,
    ForceHoPhaA,
    DongLeoTam,
    StartCheckDongDienQuaLeoAmpe,
    HadCheckedDongDienQuaLeo,
    InProgress6_1_1,
    BulongUnScrewed,
    ChuanBiLapLeoMoi,

    /// <summary>
    /// kích hoạt sự kiện khi lắp lại bọc cứng khi đã lắp lèo tạm zoomer lên dây
    /// </summary>
    ResetTransBocCungLeoTam,

    /// <summary>
    /// kích hoạt sự kiện khi lắp lại bọc cứng khi đã lắp lèo tạm endzoomer lên dây
    /// </summary>
    ResetTransBocCungLeoTamEndZoomer,
    LogMistake,
    OnSetTransItem,
    OnPhaseAOpen,
    OnPhaseAClose,
    OnPhaseALeoMoiOpen,
    OnPhaseALeoMoiClose
}
