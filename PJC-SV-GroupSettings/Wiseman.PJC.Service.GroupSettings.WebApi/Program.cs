// �N���v���O����

using Wiseman.PJC.Gen2.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// �Ɩ��t���[�����[�N�\���ǉ�
builder.AddFrameworkConfig();

var app = builder.Build();

// �f�o�b�O���s���p�X��ǉ�
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    // �f�o�b�O���s���T�[�r�X��/�o�[�W����/�R���g���[�����ŃA�N�Z�X�ł���悤�ɐݒ肵�܂��B
    app.UsePathBase("/GroupSettings/v1.0");
    app.UseRouting();
}

// �Ɩ��t���[�����[�N�\���ǉ�
app.UseFrameworkConfig();
app.UseHttpsRedirection();
app.Run();